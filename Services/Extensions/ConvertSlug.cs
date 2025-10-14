using System.Text;
using System.Text.RegularExpressions;

namespace Services.Extensions
{
    public static class ConvertSlug
    {
        public static string ToSeoUrl(this string url)
        {
            if (string.IsNullOrEmpty(url))
                return "";

            var turkishChars = new Dictionary<char, char>
            {
                { 'ı', 'i' },
                { 'ğ', 'g' },
                { 'ü', 'u' },
                { 'ş', 's' },
                { 'ö', 'o' },
                { 'ç', 'c' },
                { 'İ', 'i' },
                { 'Ğ', 'g' },
                { 'Ü', 'u' },
                { 'Ş', 's' },
                { 'Ö', 'o' },
                { 'Ç', 'c' },
            };

            var result = new StringBuilder(url.Length);
            foreach (var c in url)
            {
                result.Append(turkishChars.ContainsKey(c) ? turkishChars[c] : c);
            }

            return result
                .ToString()
                .Normalize(NormalizationForm.FormD)
                .Where(c =>
                    char.GetUnicodeCategory(c)
                    != System.Globalization.UnicodeCategory.NonSpacingMark
                )
                .ToArray() // Add this line to convert IEnumerable to array
                .AsSpan()
                .ToString()
                .ToLower()
                .Replace("&", "-and-")
                .Replace(" ", "-")
                .Replace("--", "-")
                .Trim('-')
                .Trim()
                .RegexReplace(@"[^a-z0-9\-]", "")
                .RegexReplace(@"-+", "-");
        }

        private static string RegexReplace(this string input, string pattern, string replacement)
        {
            return Regex.Replace(input, pattern, replacement);
        }
    }
}
