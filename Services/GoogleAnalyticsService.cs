using Entities.DTOs.GoogleAnalyticsDto;
using Entities.Models;
using Google.Apis.AnalyticsData.v1beta;
using Google.Apis.AnalyticsData.v1beta.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Services.Contracts;
using System.Text;

namespace Services
{
    public class GoogleAnalyticsService : IGoogleAnalyticsService
    {
        private readonly GoogleAnalyticsOptions _options;

        public GoogleAnalyticsService(IOptions<GoogleAnalyticsOptions> options)
        {
            _options = options.Value;
        }

        public async Task<AnalyticsReportResponse> GetAnalyticsReportGA4Async(AnalyticsReportRequest request)
        {
            // PropertyID'yi kontrol et
            if (string.IsNullOrEmpty(_options.PropertyId))
                throw new Exception("Google Analytics Property ID is not configured in appsettings.json.");

            if (_options.ServiceAccountKeyJson == null)
                throw new Exception("Google Analytics service account key is not configured in appsettings.json.");

            // Property ID'yi request nesnesinden veya appsettings'den al
            var propertyId = !string.IsNullOrEmpty(request.PropertyId)
                ? request.PropertyId
                : _options.PropertyId;

            // Google servis için hizmet hesabı kimlik doğrulaması
            var jsonString = JsonConvert.SerializeObject(_options.ServiceAccountKeyJson);
            var credential = GoogleCredential.FromJson(jsonString)
                .CreateScoped(AnalyticsDataService.Scope.AnalyticsReadonly);

            // Analytics Data API v1beta istemcisini oluştur (GA4 için)
            var service = new AnalyticsDataService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "API Analytics"
            });

            // API isteğini yapılandır
            var reportRequest = new RunReportRequest
            {
                Property = $"properties/{propertyId}",
                DateRanges = new List<Google.Apis.AnalyticsData.v1beta.Data.DateRange>
                {
                    new Google.Apis.AnalyticsData.v1beta.Data.DateRange
                    {
                        StartDate = request.StartDate?.ToString("yyyy-MM-dd") ?? DateTime.UtcNow.AddDays(-30).ToString("yyyy-MM-dd"),
                        EndDate = request.EndDate?.ToString("yyyy-MM-dd") ?? DateTime.UtcNow.ToString("yyyy-MM-dd")
                    }
                },
                Dimensions = request.Dimensions.Select(d => new Dimension { Name = d }).ToList(),
                Metrics = request.Metrics.Select(m => new Metric { Name = m }).ToList(),
                Limit = request.PageSize ?? 10000
            };

            if (!string.IsNullOrEmpty(request.PageToken))
            {
                if (long.TryParse(request.PageToken, out long offset))
                {
                    reportRequest.Offset = offset;
                }
            }

            var response = await service.Properties.RunReport(reportRequest, $"properties/{propertyId}").ExecuteAsync();

            var result = new AnalyticsReportResponse
            {
                DimensionHeaders = response.DimensionHeaders.Select(h => h.Name).ToList(),
                MetricHeaders = response.MetricHeaders.Select(h => h.Name).ToList(),
                RowCount = response.RowCount ?? 0,
                Rows = new List<ReportRow>()
            };

            if (response.Rows != null)
            {
                foreach (var row in response.Rows)
                {
                    var reportRow = new ReportRow
                    {
                        DimensionValues = row.DimensionValues.Select(d => d.Value).ToList(),
                        MetricValues = row.MetricValues.Select(m => new Entities.DTOs.GoogleAnalyticsDto.MetricValue { Value = m.Value }).ToList()
                    };
                    result.Rows.Add(reportRow);
                }
            }

            return result;
        }

        public async Task<AnalyticsSummary> GetAnalyticsSummaryAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            var request = new AnalyticsReportRequest
            {
                StartDate = startDate ?? DateTime.UtcNow.AddDays(-30),
                EndDate = endDate ?? DateTime.UtcNow,
                Metrics = new List<string> { "activeUsers", "screenPageViews", "sessions", "averageSessionDuration", "bounceRate" },
                Dimensions = new List<string> { "date" }
            };

            var overviewData = await GetAnalyticsReportGA4Async(request);

            // Sayfa görünümlerini al
            var topPagesRequest = new AnalyticsReportRequest
            {
                StartDate = startDate ?? DateTime.UtcNow.AddDays(-30),
                EndDate = endDate ?? DateTime.UtcNow,
                Metrics = new List<string> { "screenPageViews" },
                Dimensions = new List<string> { "pagePath", "pageTitle" },
                PageSize = 10
            };

            var topPagesData = await GetAnalyticsReportGA4Async(topPagesRequest);

            // Referans kaynakları al
            var referrersRequest = new AnalyticsReportRequest
            {
                StartDate = startDate ?? DateTime.UtcNow.AddDays(-30),
                EndDate = endDate ?? DateTime.UtcNow,
                Metrics = new List<string> { "activeUsers" },
                Dimensions = new List<string> { "sessionSource" },
                PageSize = 10
            };

            var referrersData = await GetAnalyticsReportGA4Async(referrersRequest);

            // Kullanıcı konumlarını al
            var locationsRequest = new AnalyticsReportRequest
            {
                StartDate = startDate ?? DateTime.UtcNow.AddDays(-30),
                EndDate = endDate ?? DateTime.UtcNow,
                Metrics = new List<string> { "activeUsers" },
                Dimensions = new List<string> { "country" },
                PageSize = 10
            };

            var locationsData = await GetAnalyticsReportGA4Async(locationsRequest);

            // Özet oluştur
            var summary = new AnalyticsSummary
            {
                TotalUsers = ExtractTotalMetric(overviewData, "activeUsers"),
                PageViews = ExtractTotalMetric(overviewData, "screenPageViews"),
                Sessions = ExtractTotalMetric(overviewData, "sessions"),
                AvgSessionDuration = ExtractAverageMetric(overviewData, "averageSessionDuration"),
                BounceRate = ExtractAverageMetric(overviewData, "bounceRate"),
                TopPages = ExtractTopPages(topPagesData),
                TopReferrers = ExtractTopReferrers(referrersData),
                UserLocations = ExtractUserLocations(locationsData)
            };

            return summary;
        }

        // Helper Methods
        private int ExtractTotalMetric(AnalyticsReportResponse data, string metricName)
        {
            var metricIndex = data.MetricHeaders.IndexOf(metricName);
            if (metricIndex == -1) return 0;

            int total = 0;
            foreach (var row in data.Rows)
            {
                if (int.TryParse(row.MetricValues[metricIndex].Value, out int value))
                {
                    total += value;
                }
            }
            return total;
        }

        private double ExtractAverageMetric(AnalyticsReportResponse data, string metricName)
        {
            var metricIndex = data.MetricHeaders.IndexOf(metricName);
            if (metricIndex == -1) return 0;

            double sum = 0;
            int count = 0;
            foreach (var row in data.Rows)
            {
                if (double.TryParse(row.MetricValues[metricIndex].Value, out double value))
                {
                    sum += value;
                    count++;
                }
            }
            return count > 0 ? sum / count : 0;
        }

        private List<TopPageItem> ExtractTopPages(AnalyticsReportResponse data)
        {
            var result = new List<TopPageItem>();
            var pathIndex = data.DimensionHeaders.IndexOf("pagePath");
            var titleIndex = data.DimensionHeaders.IndexOf("pageTitle");
            var viewsIndex = data.MetricHeaders.IndexOf("screenPageViews");

            if (pathIndex == -1 || viewsIndex == -1) return result;

            foreach (var row in data.Rows)
            {
                var item = new TopPageItem
                {
                    PagePath = row.DimensionValues[pathIndex],
                    PageTitle = titleIndex != -1 ? row.DimensionValues[titleIndex] : null,
                    Views = int.TryParse(row.MetricValues[viewsIndex].Value, out int views) ? views : 0
                };
                result.Add(item);
            }

            return result.OrderByDescending(p => p.Views).ToList();
        }

        private List<TopReferrerItem> ExtractTopReferrers(AnalyticsReportResponse data)
        {
            var result = new List<TopReferrerItem>();
            var sourceIndex = data.DimensionHeaders.IndexOf("sessionSource");
            var usersIndex = data.MetricHeaders.IndexOf("activeUsers");

            if (sourceIndex == -1 || usersIndex == -1) return result;

            foreach (var row in data.Rows)
            {
                var item = new TopReferrerItem
                {
                    Source = row.DimensionValues[sourceIndex],
                    Users = int.TryParse(row.MetricValues[usersIndex].Value, out int users) ? users : 0
                };
                result.Add(item);
            }

            return result.OrderByDescending(r => r.Users).ToList();
        }

        private List<UserLocationItem> ExtractUserLocations(AnalyticsReportResponse data)
        {
            var result = new List<UserLocationItem>();
            var countryIndex = data.DimensionHeaders.IndexOf("country");
            var usersIndex = data.MetricHeaders.IndexOf("activeUsers");

            if (countryIndex == -1 || usersIndex == -1) return result;

            foreach (var row in data.Rows)
            {
                var item = new UserLocationItem
                {
                    Country = row.DimensionValues[countryIndex],
                    Users = int.TryParse(row.MetricValues[usersIndex].Value, out int users) ? users : 0
                };
                result.Add(item);
            }

            return result.OrderByDescending(l => l.Users).ToList();
        }
    }
}