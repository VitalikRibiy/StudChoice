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
using System;
using X.PagedList;

namespace StudChoice.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ISubjectService subjectService;
        private readonly IFacultyService facultyService;
        private readonly IProfessorService professorService;
        private readonly ICathedraService cathedraService;
        private readonly IMapper mapper;
        private readonly int pageSize = 3;

        public AdminController(
            UserManager<User> userManagerVar,
            IMapper mapperVar,
            ISubjectService subjectServiceVar,
            IFacultyService facultyServiceVar,
            IProfessorService professorServiceVar,
            ICathedraService cathedraServiceVar
        )
        {
            userManager = userManagerVar;
            mapper = mapperVar;
            subjectService = subjectServiceVar;
            facultyService = facultyServiceVar;
            professorService = professorServiceVar;
            cathedraService = cathedraServiceVar;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Users");
        }

        #region Users
        public async Task<IActionResult> Users(int? page)
        {
            var userDtos = new List<UserDTO>();
            foreach (var user in userManager.Users)
            {
                var role = (await userManager.GetRolesAsync(user)).FirstOrDefault() ?? string.Empty;
                
                var userDto = mapper.Map<UserDTO>(user);
                userDto.Role = role;
                userDtos.Add(userDto);
            }
            int pageNumber = (page ?? 1);            
            return View(userDtos.ToPagedList(pageNumber, pageSize));
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

        #region Faculties


        public async Task<IActionResult> Faculties(int? page)
        {
            var facultyDTOs = new List<FacultyDTO>();
            foreach (var faculty in await facultyService.GetAllAsync())
            {
                var facultyDTO = mapper.Map<FacultyDTO>(faculty);
                facultyDTOs.Add(facultyDTO);
            }
            int pageNumber = (page ?? 1);
            return View(facultyDTOs.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult AddFaculty()
        {
            var facultyDTO = new FacultyDTO();
            return View(facultyDTO);
        }


        [HttpPost]
        public async Task<IActionResult> AddFaculty(FacultyDTO facultyDTO)
        {
            await facultyService.CreateAsync(facultyDTO);
            return RedirectToAction("Faculties");
        }


        public async Task<IActionResult> DeleteFaculty(int facultyId)
        {
            var faculty = await facultyService.GetAsync(facultyId);

            if (faculty != null)
            {
                await facultyService.DeleteAsync(faculty.Id);
            }

            return RedirectToAction("Faculties");
        }

        #endregion

        #region Proffesors
        public async Task<IActionResult> Professors(int? page)
        {
            var professorDTOs = new List<ProfessorDTO>();
            foreach (var professor in await professorService.GetAllAsync())
            {
                var professorDTO = mapper.Map<ProfessorDTO>(professor);
                professorDTOs.Add(professorDTO);
            }
            int pageNumber = (page ?? 1);
            return View(professorDTOs.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public async Task<IActionResult> AddProfessor()
        {
            var professorDTO = new ProfessorDTO();

            var faculties = await facultyService.GetAllAsync();
            TempData["Faculties"] = faculties;
            var cathedras = await cathedraService.GetAllAsync();
            TempData["Cathedras"] = cathedras;

            return View(professorDTO);
        }


        [HttpPost]
        public async Task<IActionResult> AddProfessor(ProfessorDTO professorDTO)
        {
            professorDTO.FacultyName = (await facultyService.GetAsync(professorDTO.FacultyId)).DisplayName;

            professorDTO.CathedraName = (await facultyService.GetAsync(professorDTO.CathedraId)).DisplayName;

            await professorService.CreateAsync(professorDTO);

            return RedirectToAction("Professors");
        }


        public async Task<IActionResult> DeleteProfessor(int professorId)
        {
            var professor = await professorService.GetAsync(professorId);

            if (professor != null)
            {
                await professorService.DeleteAsync(professor.Id);
            }

            return RedirectToAction("Professors");
        }

        #endregion

        #region Cathedra

        public async Task<IActionResult> Cathedras(int? page)
        {
            var cathedraDTOs = new List<CathedraDTO>();
            foreach (var cathedra in await cathedraService.GetAllAsync())
            {
                var cathedraDTO = mapper.Map<CathedraDTO>(cathedra);
                cathedraDTOs.Add(cathedraDTO);
            }
            int pageNumber = (page ?? 1);
            return View(cathedraDTOs.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public async Task<IActionResult> AddCathedra()
        {
            var cathedraDTO = new CathedraDTO();
            var faculties = await facultyService.GetAllAsync();
            TempData["Faculties"] = faculties;
            return View(cathedraDTO);
        }


        [HttpPost]
        public async Task<IActionResult> AddCathedra(CathedraDTO cathedraDTO)
        {
            cathedraDTO.FacultyName = (await facultyService.GetAsync(cathedraDTO.FacultyId)).DisplayName;
            await cathedraService.CreateAsync(cathedraDTO);
            return RedirectToAction("Cathedras");
        }


        public async Task<IActionResult> DeleteCathedra(int cathedraId)
        {
            var cathedra = await cathedraService.GetAsync(cathedraId);

            if (cathedra != null)
            {
                await cathedraService.DeleteAsync(cathedra.Id);
            }

            return RedirectToAction("Cathedras");
        }

        #endregion

        #region Subjects

        public async Task<IActionResult> Subjects(int? page)
        {
            var subjectDTOs = new List<SubjectDTO>();
            foreach (var subject in await subjectService.GetAllAsync())
            {
                var type = subject.Type != null ? subject.Type : string.Empty;

                var subjectDTO = mapper.Map<SubjectDTO>(subject);
                subjectDTO.Type = type;
                subjectDTOs.Add(subjectDTO);
            }
            int pageNumber = (page ?? 1);
            return View(subjectDTOs.ToPagedList(pageNumber, pageSize));            
        }

        [HttpGet]
        public async Task<IActionResult> AddSubject()
        {
            var subjectDTO = new SubjectDTO();

            var faculties = await facultyService.GetAllAsync();
            TempData["Faculties"] = faculties;
            var cathedras = await cathedraService.GetAllAsync();
            TempData["Cathedras"] = cathedras;
            var professors = await professorService.GetAllAsync();
            TempData["Professors"] = professors;

            return View(subjectDTO);
        }


        [HttpPost]
        public async Task<IActionResult> AddSubject(SubjectDTO subjectDTO)
        {
            subjectDTO.FacultyName = (await facultyService.GetAsync(subjectDTO.FacultyId)).DisplayName;

            subjectDTO.CathedraName = (await cathedraService.GetAsync(subjectDTO.CathedraId)).DisplayName;

            subjectDTO.ProfessorFullName = (await professorService.GetAsync(subjectDTO.CathedraId)).FullName;

            await subjectService.CreateAsync(subjectDTO);

            return RedirectToAction("Subjects");
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