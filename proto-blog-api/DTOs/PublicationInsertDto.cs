namespace proto_blog_api.DTOs
{
    public class PublicationInsertDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Boolean Deleted { get; set; }
        public List<int> AuthorIds { get; set; } = new List<int>();

    }
}
