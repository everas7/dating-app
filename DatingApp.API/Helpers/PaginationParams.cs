namespace DatingApp.API.Helpers
{
    public interface IPaginationParams
    {
        int MaxPerPage { get;}
        int Page { get; set; }
        int PerPage
        {
            get;
            set;
        }
    }
}