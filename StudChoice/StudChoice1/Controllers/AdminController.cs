﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudChoice.BLL.DTOs;
using StudChoice.BLL.Services.Interfaces;

namespace StudChoice.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly ISubjectService _subjectService;
        private readonly IMapper _mapper;

        public AdminController(UserManager<IdentityUser<int>> userManager, IMapper mapper, ISubjectService subjectService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _subjectService = subjectService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Users");
        }

        #region Users
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

        #endregion

        #region Subjects

        public async Task<IActionResult> Subjects()
        {
            var subjectDTOs = new List<SubjectDTO>();
            foreach (var subject in await _subjectService.GetAllAsync())
            {
                var type = subject.Type != null ? subject.Type : string.Empty;

                var subjectDTO = _mapper.Map<SubjectDTO>(subject);
                subjectDTO.Type = type;
                subjectDTOs.Add(subjectDTO);
            }

            return View(subjectDTOs);
        }

        public async Task<IActionResult> DeleteSubject(int subjectId)
        {
            var subject = await _subjectService.GetAsync(subjectId);

            if (subject != null)
            {
                await _subjectService.DeleteAsync(subject.Id);
            }

            return RedirectToAction("Subjects");
        }

        public async Task<IActionResult> SetSubjectType(int subjectId, string type)
        {
            var subject = await _subjectService.GetAsync(subjectId);
            if (subject != null && subject.Type != type)
            {
                subject.Type = type;
                await _subjectService.UpdateAsync(subject);
            }

            return RedirectToAction("Subjects");
        }

        #endregion
    }
}