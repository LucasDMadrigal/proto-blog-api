using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using proto_blog_api.Utils;

namespace proto_blog_api.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }=string.Empty;
        //public string Password { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
        public UserRole Role { get; set; } = UserRole.Lector;
        public ICollection<Publications> Publications { get; set; } = new List<Publications>();
    }
}
