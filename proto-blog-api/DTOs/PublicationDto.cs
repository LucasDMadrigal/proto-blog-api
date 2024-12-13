namespace proto_blog_api.DTOs
{
    public class PublicationDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Boolean Deleted { get; set; }

    }
}
