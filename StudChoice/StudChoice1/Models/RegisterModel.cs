using System.ComponentModel.DataAnnotations;

namespace StudChoice1.Models
{
    public class RegisterModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string TransictionNumber { get; set; }
    }
}
