using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanDienThoai.Models
{
    [Table("Users")]
    public class UserAccount
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [StringLength(255)]
        public string Password { get; set; }

        [StringLength(255)]
        public string PasswordHash { get; set; }

        [StringLength(255)]
        public string PasswordSalt { get; set; }

        [StringLength(50)]
        public string Role { get; set; }

        [StringLength(255)]
        public string FullName { get; set; }

        [StringLength(50)]
        public string PhoneNumber { get; set; }
    }
}
