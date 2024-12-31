using System.ComponentModel.DataAnnotations;

namespace proto_blog_api.DTOs
{
    public class PublicationInsertDto
    {
        [Required(ErrorMessage ="El titulo es requerido")]
        public string Title { get; set; }
        
        [Required(ErrorMessage ="El contenido es requerido")]
        public string Content { get; set; }
        public Boolean Deleted { get; set; }

        [Required(ErrorMessage ="debe incluir al menos un autor")]
        [MinLength(1,ErrorMessage ="debe incluir al menos un autor")]
        public ICollection<int> AuthorIds { get; set; } = new List<int>();

    }
}
