namespace proto_blog_api.DTOs
{
    public class UserInsertDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int? AuthorId { get; set; }
    }
}

