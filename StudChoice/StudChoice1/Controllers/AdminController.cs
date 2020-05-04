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
        public async Task<IActionResult> Users(int? page, UserFilterParams userFilterParams)
        {
            var userDtos = new List<UserDTO>();
            foreach (var user in userManager.Users)
            {
                var role = (await userManager.GetRolesAsync(user)).FirstOrDefault() ?? string.Empty;

                var userDto = mapper.Map<UserDTO>(user);
                userDto.Role = role;
                userDtos.Add(userDto);
            }
            ViewBag.Roles = userDtos.Select(x => x.Role).Distinct().ToList();
            if (userFilterParams.Name != null || userFilterParams.Surname !=null || userFilterParams.Email!=null || userFilterParams.Role != null)
            {
                userDtos = GetFilteredUsers(userDtos, userFilterParams);
                ViewBag.FilterParams = userFilterParams;
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


        public async Task<IActionResult> Faculties(int? page, FacultyFilterParams facultyFilterParams)
        {
            var facultyDTOs = new List<FacultyDTO>();
            foreach (var faculty in await facultyService.GetAllAsync())
            {
                var facultyDTO = mapper.Map<FacultyDTO>(faculty);
                facultyDTOs.Add(facultyDTO);
            }
            if (facultyFilterParams.Name != null )
            {
                facultyDTOs = GetFilteredFaculties(facultyDTOs, facultyFilterParams);
                ViewBag.FilterParams = facultyFilterParams;
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
        public async Task<IActionResult> Professors(int? page, ProfessorFilterParams professorFilterParams)
        {
            var professorDTOs = new List<ProfessorDTO>();
            foreach (var professor in await professorService.GetAllAsync())
            {
                var professorDTO = mapper.Map<ProfessorDTO>(professor);
                professorDTOs.Add(professorDTO);
            }
            ViewBag.Faculties = professorDTOs.Select(x => x.FacultyName).Distinct().ToList();
            ViewBag.Cathedras = professorDTOs.Select(x => x.CathedraName).Distinct().ToList();
            if (professorFilterParams.FirstName != null || professorFilterParams.MiddleName != null || professorFilterParams.LastName != null || professorFilterParams.FacultyName != null || professorFilterParams.CathedraName != null )
            {
                professorDTOs = GetFilteredProfessors(professorDTOs, professorFilterParams);
                ViewBag.FilterParams = professorFilterParams;
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

        public async Task<IActionResult> Cathedras(int? page,CathedraFilterParams cathedraFilterParams)
        {
            var cathedraDTOs = new List<CathedraDTO>();
            foreach (var cathedra in await cathedraService.GetAllAsync())
            {
                var cathedraDTO = mapper.Map<CathedraDTO>(cathedra);
                cathedraDTOs.Add(cathedraDTO);
            }
            if (cathedraFilterParams.Name != null)
            {
                cathedraDTOs = GetFilteredCathedras(cathedraDTOs, cathedraFilterParams);
                ViewBag.FilterParams = cathedraFilterParams;
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

        public async Task<IActionResult> Subjects(int? page, SubjectFilterParams subjectFilterParams)
        {
            var subjectDTOs = (await subjectService.GetAllAsync()).ToList();
            ViewBag.Faculties = subjectDTOs.Select(x => x.FacultyName).Distinct().ToList();
            ViewBag.Types = subjectDTOs.Select(x => x.Type).Distinct().ToList();
            if (subjectFilterParams.Name != null || subjectFilterParams.FacultyName != null || subjectFilterParams.MinStudents != null || subjectFilterParams.MaxStudents != null || subjectFilterParams.Professor != null || subjectFilterParams.Type != null)
            {
                subjectDTOs = GetFilteredSubjects(subjectDTOs, subjectFilterParams);
                ViewBag.FilterParams = subjectFilterParams;
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

        #region Filtering
        private List<UserDTO> GetFilteredUsers(List<UserDTO> userDtos, UserFilterParams userFilterParams)
        {
            if (!String.IsNullOrEmpty(userFilterParams.Name))
            {
                userDtos = userDtos.Where(s => (string.IsNullOrEmpty(s.FirstName) ? false : s.FirstName.ToLower().Contains(userFilterParams.Name.ToLower()))).ToList();
            }
            if (!String.IsNullOrEmpty(userFilterParams.Surname))
            {
                userDtos = userDtos.Where(s => (string.IsNullOrEmpty(s.LastName) ? false : s.LastName.ToLower().Contains(userFilterParams.Surname.ToLower()))).ToList();
            }
            if (!String.IsNullOrEmpty(userFilterParams.Email))
            {
                userDtos = userDtos.Where(s => (string.IsNullOrEmpty(s.Email) ? false : s.Email.ToLower().Contains(userFilterParams.Email.ToLower()))).ToList();
            }
            if (!String.IsNullOrEmpty(userFilterParams.Role) && userFilterParams.Role != "0")
            {
                userDtos = userDtos.Where(s => (string.IsNullOrEmpty(s.Role) ? false : s.Role.ToLower().Contains(userFilterParams.Role.ToLower()))).ToList();
            }
            if (!String.IsNullOrEmpty(userFilterParams.TransictionCode))
            {
                userDtos = userDtos.Where(s => (string.IsNullOrEmpty(s.UserName) ? false : s.UserName.ToLower().Contains(userFilterParams.TransictionCode.ToLower()))).ToList();
            }
            return userDtos;
        }

        private List<SubjectDTO> GetFilteredSubjects(List<SubjectDTO> subjectDtos, SubjectFilterParams subjectFilterParams)
        {
            if (!String.IsNullOrEmpty(subjectFilterParams.Name))
            {
                subjectDtos = subjectDtos.Where(s => (string.IsNullOrEmpty(s.Name) ? false : s.Name.ToLower().Contains(subjectFilterParams.Name.ToLower()))).ToList();
            }
            if (!String.IsNullOrEmpty(subjectFilterParams.Type)&& subjectFilterParams.Type!="0")
            {
                subjectDtos = subjectDtos.Where(s => (string.IsNullOrEmpty(s.Type) ? false : s.Type==subjectFilterParams.Type)).ToList();
            }
            if (!String.IsNullOrEmpty(subjectFilterParams.Professor))
            {
                subjectDtos = subjectDtos.Where(s => (string.IsNullOrEmpty(s.ProfessorFullName) ? false : s.ProfessorFullName.ToLower().Contains(subjectFilterParams.Professor.ToLower()))).ToList();
            }
            if (!String.IsNullOrEmpty(subjectFilterParams.MinStudents))
            {
                subjectDtos = subjectDtos.Where(s => (string.IsNullOrEmpty(s.MinStudents.ToString()) ? false : s.MinStudents >= Convert.ToInt32(subjectFilterParams.MinStudents))).ToList();
            }
            if (!String.IsNullOrEmpty(subjectFilterParams.MaxStudents))
            {
                subjectDtos = subjectDtos.Where(s => (string.IsNullOrEmpty(s.MaxStudents.ToString()) ? false : s.MaxStudents <= Convert.ToInt32(subjectFilterParams.MaxStudents))).ToList();
            }
            if (!String.IsNullOrEmpty(subjectFilterParams.FacultyName) && subjectFilterParams.FacultyName != "0")
            {
                subjectDtos = subjectDtos.Where(s => (string.IsNullOrEmpty(s.FacultyName) ? false : s.FacultyName==subjectFilterParams.FacultyName)).ToList();
            }
            return subjectDtos;
        }
        private List<FacultyDTO> GetFilteredFaculties(List<FacultyDTO> facultyDTOs, FacultyFilterParams facultyFilterParams)
        {
            if (!String.IsNullOrEmpty(facultyFilterParams.Name))
            {
                facultyDTOs = facultyDTOs.Where(s => (string.IsNullOrEmpty(s.DisplayName) ? false : s.DisplayName.ToLower().Contains(facultyFilterParams.Name.ToLower()))).ToList();
            }            
            return facultyDTOs;
        }
        private List<CathedraDTO> GetFilteredCathedras(List<CathedraDTO> cathedraDTOs, CathedraFilterParams cathedraFilterParams)
        {
            if (!String.IsNullOrEmpty(cathedraFilterParams.Name))
            {
                cathedraDTOs = cathedraDTOs.Where(s => (string.IsNullOrEmpty(s.DisplayName) ? false : s.DisplayName.ToLower().Contains(cathedraFilterParams.Name.ToLower()))).ToList();
            }
            return cathedraDTOs;
        }
        private List<ProfessorDTO> GetFilteredProfessors(List<ProfessorDTO> professorDTOs, ProfessorFilterParams professorFilterParams)
        {
            if (!String.IsNullOrEmpty(professorFilterParams.FirstName))
            {
                professorDTOs = professorDTOs.Where(s => (string.IsNullOrEmpty(s.FirstName) ? false : s.FirstName.ToLower().Contains(professorFilterParams.FirstName.ToLower()))).ToList();
            }
            if (!String.IsNullOrEmpty(professorFilterParams.CathedraName) && professorFilterParams.CathedraName != "0")
            {
                professorDTOs = professorDTOs.Where(s => (string.IsNullOrEmpty(s.FacultyName) ? false : s.CathedraName == professorFilterParams.CathedraName)).ToList();
            }
            if (!String.IsNullOrEmpty(professorFilterParams.MiddleName))
            {
                professorDTOs = professorDTOs.Where(s => (string.IsNullOrEmpty(s.MiddleName) ? false : s.MiddleName.ToLower().Contains(professorFilterParams.MiddleName.ToLower()))).ToList();
            }
            if (!String.IsNullOrEmpty(professorFilterParams.FacultyName) && professorFilterParams.FacultyName != "0")
            {
                professorDTOs = professorDTOs.Where(s => (string.IsNullOrEmpty(s.FacultyName) ? false : s.FacultyName == professorFilterParams.FacultyName)).ToList();
            }
            if (!String.IsNullOrEmpty(professorFilterParams.LastName))
            {
                professorDTOs = professorDTOs.Where(s => (string.IsNullOrEmpty(s.LastName) ? false : s.LastName.ToLower().Contains(professorFilterParams.LastName.ToLower()))).ToList();
            }
            return professorDTOs;
        }
        #endregion

        #region Distribution

        public async void Distribution()
        {
            var userDTOs = new List<UserDTO>();
            foreach (var user in userManager.Users)
            {
                var role = (await userManager.GetRolesAsync(user)).FirstOrDefault() ?? string.Empty;

                var userDto = mapper.Map<UserDTO>(user);
                userDto.Role = role;
                if (userDto.Role == "2")
                    userDTOs.Add(userDto);
            }
            var subjects = await subjectService.GetAllAsync();
            foreach (Course course in Enum.GetValues(typeof(Course)))
            {
                DistributeDvvs(userDTOs.Where(x => x.Course == course && x.Dvvs1Id == null).ToList(), subjects.Where(x => x.Course == course && x.AssignedStudentsCount < x.MaxStudents && x.Term == Term.First).ToList(), Term.First);
                DistributeDvvs(userDTOs.Where(x => x.Course == course && x.Dvvs2Id == null).ToList(), subjects.Where(x => x.Course == course && x.AssignedStudentsCount < x.MaxStudents && x.Term == Term.Second).ToList(), Term.Second);
            }
        }

        public async void DistributeDvvs(List<UserDTO> userDTOs,List<SubjectDTO> subjectDTOs,Term term)
        {
            var users = SortDVVS(userDTOs);
            var modifiedUsers = new List<UserDTO>();
            var modifiedSubjects = new List<SubjectDTO>();
            foreach(var subject in subjectDTOs.OrderBy(x=>x.MaxStudents-x.AssignedStudentsCount))
            {
                if (userDTOs.Count == 0)
                    break;
                while (subject.AssignedStudentsCount != subject.MaxStudents)
                {
                    if (userDTOs.Count == 0)
                        break;
                    var user = userDTOs.FirstOrDefault(x => x.FacultyId == subject.FacultyId);
                    if (user == null)
                        user = userDTOs.FirstOrDefault();
                    if(term == Term.First)
                    {
                        user.Dvvs1Id = subject.Id;
                    }
                    else if(term == Term.Second)
                    {
                        user.Dvvs2Id = subject.Id;
                    }
                    else
                    {
                        break;
                    }
                    subject.AssignedStudentsCount++;
                    modifiedUsers.Add(user);
                    userDTOs.Remove(user);
                }
                modifiedSubjects.Add(subject);
            }
            modifiedSubjects.ForEach(x=>subjectService.UpdateAsync(x));
            foreach(var userDTO in modifiedUsers)
            {
                await userManager.UpdateAsync(mapper.Map<User>(userDTO));
            }
            foreach(var subjectDTO in modifiedSubjects)
            {
                await subjectService.UpdateAsync(subjectDTO);
            }
        }

        public List<UserDTO> SortDVVS(List<UserDTO> userDTOs)
        {
            return userDTOs.OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ThenBy(x => x.MiddleName).ToList();
        }

        public IEnumerable<IGrouping<int,UserDTO>> SortDV(List<UserDTO> userDTOs)
        {
            return userDTOs.OrderByDescending(x => x.AvaragePoints).ThenBy(x => x.LastName).GroupBy(x => x.FacultyId);
        }

        #endregion
    }
}