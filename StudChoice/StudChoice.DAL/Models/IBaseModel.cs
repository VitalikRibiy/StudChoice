using System.ComponentModel.DataAnnotations;

namespace StudChoice.DAL.Models
{
    public interface IBaseModel
    {
        [Required]
        public int Id { get; set; }
    }
}
