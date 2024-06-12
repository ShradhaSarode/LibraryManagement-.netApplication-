using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Models
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public int BookID { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public int AuthorID { get; set; }
        [NotMapped]
        public string? Name { get; set; }
        [Required]
        public int PublishedYear { get; set; }
        [Required]
        [MaxLength(100)]
        public string CoverImage { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }

        public int? CategoryId { get; set; }
        [NotMapped]
        public string? Categoryname { get; set; }

        
    }
}
