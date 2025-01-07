using proto_blog_api.Utils;

namespace proto_blog_api.DTOs
{
    public class UserUpdateDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;

        public UserRole Role { get; set; }
    }
}
