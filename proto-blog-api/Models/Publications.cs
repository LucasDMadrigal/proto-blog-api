﻿namespace proto_blog_api.Models
{
    public class Publications
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Boolean Deleted { get; set; }
        public ICollection<Author> Authors { get; set; } = new List<Author>(); // Relación M:N con Authors

    }
}
