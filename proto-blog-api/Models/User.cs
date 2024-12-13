using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace proto_blog_api.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }=string.Empty;
        public string Password { get; set; } = string.Empty;

        public int? AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public Author? Author { get; set; }
    }
}
