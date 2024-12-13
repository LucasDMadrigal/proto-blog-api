namespace proto_blog_api.DTOs
{
    public class PublicationUpdateDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Boolean Deleted { get; set; }
    }
}
