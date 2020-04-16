using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudChoice.BLL.ViewModels;

namespace StudChoice.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser<int>> userManager;
        private readonly IMapper mapper;

        public AdminController(UserManager<IdentityUser<int>> userManagerVar, IMapper mapperVar)
        {
            userManager = userManagerVar;
            mapper = mapperVar;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Users");
        }

        public async Task<IActionResult> Users()
        {
            var userVMs = new List<UserVM>();
            foreach (var user in userManager.Users)
            {
                var role = (await userManager.GetRolesAsync(user)).FirstOrDefault() ?? string.Empty;
                
                var userVM = mapper.Map<UserVM>(user);
                userVM.Role = role;
                userVMs.Add(userVM);
            }

            return View(userVMs);
        }

        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            
            if (user != null)
            {
                await userManager.DeleteAsync(user);
            }

            return RedirectToAction("Users");
        }

        public async Task<IActionResult> SetUserRole(string userId, string roleName)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user != null && !(await userManager.IsInRoleAsync(user, roleName)))
            {
                await userManager.RemoveFromRolesAsync(user, new string[] { "Admin", "User" });
                await userManager.AddToRoleAsync(user, roleName);
            }

            return RedirectToAction("Users");
        }
    }
}