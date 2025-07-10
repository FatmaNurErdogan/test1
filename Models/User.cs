using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace test1.Models
{
    [Table("User")]
    public class User
    {
        public int userID { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
