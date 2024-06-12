using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Models
{
    [Table("Authors")]
    public class Author
    {
        [Key]
        public int AuthorID { get; set; }

        [Required]
        public string? Name { get; set; }
    }
}
