using System.ComponentModel.DataAnnotations;

namespace WebApiSample.Models
{
    public class Book : Entity
    {
        [Required]
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public string Description { get; set; }
    }
}