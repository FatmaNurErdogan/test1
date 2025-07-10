using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace test1.Models
{
    [Table("Borrowed")]
    public class Borrowed
    {
        [Key]
        public int BorrowedID { get; set; }
        [Column("bookID")]
        public int BookId { get; set; }
        [Column("userID")]
        public int UserId { get; set; }
        [Column("date of borrow")]
        public DateTime BorrowDate { get; set; }
        [Column("date of return")]
        public DateTime? ReturnDate { get; set; }
    }
}


