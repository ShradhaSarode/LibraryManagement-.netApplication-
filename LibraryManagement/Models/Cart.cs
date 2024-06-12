using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Models
{
    [Table("Cart")]
    public class Cart
    {
        [Key]
        public int CartID { get; set; }

        public int BookID { get; set; }

        public int UserID { get; set; }

        public int Quantity { get; set; }
    }
}
