using System;
using System.ComponentModel.DataAnnotations;

namespace Persistence.Entities
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime? Birthdate { get; set; }
    }

}
