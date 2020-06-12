namespace DatingApp.API.Helpers
{
    public class PaginationParams
    {
        private const int MaxPerPage = 50;
        public int Page { get; set; } = 1;
        private int perPage = 10;
        public int PerPage
        {
            get { return perPage; }
            set { perPage = (value <= MaxPerPage) ? value : MaxPerPage; }
        }
    }
}