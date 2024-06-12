using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Models
{
    [Table("OrderItems")]
    public class OrderItems
    {
        [Key]
        public int OrderItemID { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }
        public int OrderStatusId { get; set; }
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }

        [ForeignKey("OrderId")]
        public Orders Order { get; set; }

        [ForeignKey("ProductId")]
        public Book Book { get; set; }

        [ForeignKey("OrderStatusId")]
        public OrderStatus OrderStatus { get; set; }
    }
}
