using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace proto_blog_api.Models
{
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;


        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
        public ICollection<Publications> Publications { get; set; } = new List<Publications>(); // Relación 1:N con Publications

    }
}
