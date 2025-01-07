using System.ComponentModel.DataAnnotations;

namespace proto_blog_api.DTOs
{
    public class LoginDto
    {
        [Required]
        public string username {  get; set; } = String.Empty;
        [Required]
        public string password { get; set; } = String.Empty;
    }
}
