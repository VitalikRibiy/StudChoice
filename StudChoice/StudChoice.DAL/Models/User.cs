using Microsoft.AspNetCore.Identity;

namespace StudChoice.DAL.Models
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }

        public string Surname { get; set; }
    }
}