using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudChoice.BLL.DTOs;
using StudChoice.BLL.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using StudChoice.DAL.Models;

namespace StudChoice.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ISubjectService subjectService;
        private readonly IMapper mapper;

        public AdminController(UserManager<User> userManagerVar, IMapper mapperVar, ISubjectService subjectServiceVar)
        {
            userManager = userManagerVar;
            mapper = mapperVar;
            subjectService = subjectServiceVar;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Users");
        }

        #region Users
        public async Task<IActionResult> Users()
        {
            var userDtos = new List<UserDTO>();
            foreach (var user in userManager.Users)
            {
                var role = (await userManager.GetRolesAsync(user)).FirstOrDefault() ?? string.Empty;
                
                var userDto = mapper.Map<UserDTO>(user);
                userDto.Role = role;
                userDtos.Add(userDto);
            }

            return View(userDtos);
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

        [HttpGet]
        public IActionResult AddUser()
        {
            var userDto = new UserDTO();
            return View(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserDTO userDto)
        {
            var user = mapper.Map<User>(userDto);
            await userManager.CreateAsync(user, "Test123");

            await userManager.AddToRoleAsync(user, userDto.Role);

            return RedirectToAction("Users");
        }

        #endregion

        #region Subjects

        public async Task<IActionResult> Subjects()
        {
            var subjectDTOs = new List<SubjectDTO>();
            foreach (var subject in await subjectService.GetAllAsync())
            {
                var type = subject.Type != null ? subject.Type : string.Empty;

                var subjectDTO = mapper.Map<SubjectDTO>(subject);
                subjectDTO.Type = type;
                subjectDTOs.Add(subjectDTO);
            }

            return View(subjectDTOs);
        }

        public async Task<IActionResult> DeleteSubject(int subjectId)
        {
            var subject = await subjectService.GetAsync(subjectId);

            if (subject != null)
            {
                await subjectService.DeleteAsync(subject.Id);
            }

            return RedirectToAction("Subjects");
        }

        public async Task<IActionResult> SetSubjectType(int subjectId, string type)
        {
            var subject = await subjectService.GetAsync(subjectId);
            if (subject != null && subject.Type != type)
            {
                subject.Type = type;
                await subjectService.UpdateAsync(subject);
            }

            return RedirectToAction("Subjects");
        }

        #endregion
    }
}