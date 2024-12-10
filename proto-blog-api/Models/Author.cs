namespace proto_blog_api.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public User? User { get; set; }
    }
}
