namespace OigaDpr.Web.Models
{
    public class PaginatedUserList
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public List<User> Users { get; set; } = new List<User>();
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
    }
}
