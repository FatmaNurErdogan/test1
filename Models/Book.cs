using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace test1.Models
{
    [Table("Book")]
    public class Book
    {

        [Key]
        [Column("Book ID")]
        public int BookID { get; set; }

        [Column("book name")]
        public string BookName { get; set; }

        [Column("author")]
        public string Author { get; set; }

        [Column("publisher")]
        public string Publisher { get; set; }

        [Column("type")]
        public string Type { get; set; }

        [Column("stok")]
        public int Stok { get; set; }
    }
}
