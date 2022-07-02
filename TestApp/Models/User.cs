using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApp.Models
{
    public class User
    {
        [DatabaseGenerat‌ed(DatabaseGeneratedOp‌​tion.None)]
        public int Id { get; set; }
        [Required]
        public string Token { get; set; }
    }
}