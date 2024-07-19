namespace SmartSchool.WebAPI.Helpers
{
    public class PaginationHeader
    {
        public int CurrentPage {get; set; }
        public int ItemsPerPage { get; set; } 
        public int PageItems { get; set; }
        public int TotalPages { get; set; }

        public PaginationHeader(int currentPage, int pageItems, int itemsPerPage, int totalPages) 
        {
            this.CurrentPage = currentPage;
            this.PageItems = pageItems;
            this.ItemsPerPage = itemsPerPage;
            this.TotalPages = totalPages;
        }
    }
}