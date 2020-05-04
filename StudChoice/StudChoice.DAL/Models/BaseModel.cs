using System.ComponentModel.DataAnnotations;

namespace StudChoice.DAL.Models
{
    public class BaseModel : IBaseModel
    {
        [Required]
        public int Id { get; set; }
    }
}
