using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudChoice.DAL.Models
{
    class StudChoiceSMUser : IdentityUser
    {
        string TransictionNumber { get; set; }
    }
}
