namespace Entities.RequestFeature
{
    public class MetaData
    {
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public bool HasPrev => CurrentPage > 1;
        public bool HasNext => TotalPage > CurrentPage;
    }
}
