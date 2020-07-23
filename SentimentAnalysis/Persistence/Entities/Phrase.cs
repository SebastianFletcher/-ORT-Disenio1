using BusinessLogic.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Persistence.Entities
{
    public class Phrase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Word { get; set; }

        public SentimentType? Type { get; set; }

        public virtual Entity Entity { get; set; }

        [Required]
        public DateTime PostedDate { get; set; }

        public SentimentGrade? Grade { get; set; }

        [Required]
        public virtual Author Author { get; set; }
    }
}
