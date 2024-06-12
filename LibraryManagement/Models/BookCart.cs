namespace LibraryManagement.Models
{
    public class BookCart
    {
        public int CartID { get; set; }
        public int BookID { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }
        public string CoverImage { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
