using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace test1.Models
{
    public class AddAuthor
    {
        public string Name { get; set; } = string.Empty;

        public string? Biography { get; set; }

        public int? BirthYear { get; set; }
    }
}
