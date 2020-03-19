using Microsoft.AspNetCore.Identity;

namespace StudChoice.DAL.Models
{
    class StudChoiceSMUser : IdentityUser
    {
        string TransictionNumber { get; set; }
    }
}
