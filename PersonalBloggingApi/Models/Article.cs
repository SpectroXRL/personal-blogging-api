using System.ComponentModel.DataAnnotations;

namespace PersonalBloggingApi.Models
{
    public class Article
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Body { get; set; }

        [Required]
        public string? Author { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
