using System.ComponentModel.DataAnnotations;

namespace PersonalBloggingApi.Models
{
    public class Article
    {
        public int Id { get; set; }

        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Body { get; set; }

        [Required]
        public required string Author { get; set; }

        public DateOnly CreatedAt { get; set; }

        public DateOnly LastEdited { get; set; }
    }
}
