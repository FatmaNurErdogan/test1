using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace test1.Models
{
    [Table("Author")]
    public class Author
    {
        [Key]
        public int AuthorID { get; set; }

        [Column("Name")]
        public string? Name { get; set; } // ← null olabilir dedik

        [Column("Biography", TypeName = "nvarchar(MAX)")]
        public string? Biography { get; set; }

        [Column("Birthyear")]
        public int? BirthYear { get; set; }

        public List<Book>? Books { get; set; }
    }

}
