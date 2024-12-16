namespace proto_blog_api.Models
{
    public class Publications
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Boolean Deleted { get; set; }
        public ICollection<User> Authors { get; set; } = new List<User>(); // Relación M:N con Authors

    }
}
