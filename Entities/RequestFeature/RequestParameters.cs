namespace Entities.RequestFeature
{
    public class RequestParameters
    {
        const int maxPageSize = 10;

        public int PageNumber { get; set; }

        private int _pageSize;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > maxPageSize ? value : maxPageSize; }
        }
    }
}
