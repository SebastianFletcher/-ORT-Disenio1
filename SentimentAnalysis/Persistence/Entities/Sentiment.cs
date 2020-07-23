using BusinessLogic.Enums;
using System.ComponentModel.DataAnnotations;

namespace Persistence.Entities
{
    public class Sentiment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public SentimentType Type { get; set; }

        [Required]
        public string Word { get; set; }
    }
}
