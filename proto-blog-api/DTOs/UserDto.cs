﻿namespace proto_blog_api.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int? AuthorId { get; set; }

    }
}
