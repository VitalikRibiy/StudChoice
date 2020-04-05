using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudChoice.BLL.DTOs;

namespace StudChoice.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly IMapper _mapper;

        public AdminController(UserManager<IdentityUser<int>> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Users");
        }

        public async Task<IActionResult> Users()
        {
            var userVMs = new List<UserDTO>();
            foreach (var user in _userManager.Users)
            {
                var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? string.Empty;
                
                var userVM = _mapper.Map<UserDTO>(user);
                userVM.Role = role;
                userVMs.Add(userVM);
            }

            return View(userVMs);
        }

        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            return RedirectToAction("Users");
        }

        public async Task<IActionResult> SetUserRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null && !(await _userManager.IsInRoleAsync(user, roleName)))
            {
                await _userManager.RemoveFromRolesAsync(user, new string[] { "Admin", "User" });
                await _userManager.AddToRoleAsync(user, roleName);
            }

            return RedirectToAction("Users");
        }
    }
}