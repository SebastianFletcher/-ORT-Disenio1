using BusinessLogic.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Persistence.Entities
{
    public class Alarm
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public int PostCount { get; set; }

        [Required]
        public int PostQuantity { get; set; }
        
        [Required]
        public SentimentType Type { get; set; }

        [Required]
        public int Time { get; set; }
    }
}
