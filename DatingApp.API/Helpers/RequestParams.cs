namespace DatingApp.API.Helpers
{
    public class RequestParams<T> : ISortingParams<T>, IPaginationParams
    {
        public T SortBy { get; set; }
        public SortOrder SortOrder { get; set; } = SortOrder.Desc;
        public int MaxPerPage { get; } = 50;
        private int perPage = 5;
        public int Page { get; set; }
        public int PerPage
        {
            get => perPage;
            set { perPage = (value > MaxPerPage) ? MaxPerPage : value; }
        }
    }
}