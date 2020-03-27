using System.ComponentModel.DataAnnotations;

namespace StudChoice1.Models
{
    public class InputModel 
    {
        [Required]
        public string TransictionNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
