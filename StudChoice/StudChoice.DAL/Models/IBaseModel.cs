using System.ComponentModel.DataAnnotations;

namespace StudChoice.DAL.Models
{
    public interface IBaseModel
    {
        [Required]
        public long Id { get; set; }
    }
}
