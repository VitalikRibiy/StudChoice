using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudChoice.BLL.DTOs;
using StudChoice.BLL.Services.Interfaces;
using StudChoice.DAL.Models;
using StudChoice.Models;
using StudChoice1.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudChoice.Controllers
{
    public class SubjectController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ISubjectService subjectService;
        private readonly ILogger<HomeController> logger;

        public SubjectController(ILogger<HomeController> loggerVar, ISubjectService subjVar, UserManager<User> userManagerVar, SignInManager<User> signInManagerVar)
        {
            logger = loggerVar;
            userManager = userManagerVar;
            signInManager = signInManagerVar;
            subjectService = subjVar;
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Subjects(string subjectType, string term)
        {
            var subjectDTOs = (await subjectService.GetAllAsync()).ToList();

            var user = await userManager.GetUserAsync(User);

            if(subjectType == "ДВ")
            {
                subjectDTOs = subjectDTOs.Where(x => x.Type == subjectType && x.Course == user.Course && x.Term.GetDescription() == term && x.FacultyId == user.FacultyId).ToList();
            }
            else if(subjectType == "ДВВС")
            {
                subjectDTOs = subjectDTOs.Where(x => x.Type == subjectType && x.Course == user.Course && x.Term.GetDescription() == term).ToList();
            }
            else
            {
                throw (new Exception("Wrong subject type!"));
            }            

            return View("Subjects", subjectDTOs);
        }

        public async Task<IActionResult> ViewSubject(string subjectId)
        {
            var subject = await subjectService.GetAsync(Int32.Parse(subjectId));

            return View("ViewSubject", subject);
        }

        public async Task<IActionResult> Choose(string subjectId)
        {
            var user = await userManager.GetUserAsync(User);

            var subject = await subjectService.GetAsync(Int32.Parse(subjectId));

            if(subject.Term.GetDescription() == "First")
            {
                if(subject.Type == "ДВ")
                {
                    user.Dv1Id = Int32.Parse(subjectId);
                }
                else if(subject.Type == "ДВВС")
                {
                    user.Dvvs1Id = Int32.Parse(subjectId);
                }
            }
            else if(subject.Term.GetDescription() == "Second")
            {
                if (subject.Type == "ДВ")
                {
                    user.Dv2Id = Int32.Parse(subjectId);
                }
                else if (subject.Type == "ДВВС")
                {
                    user.Dvvs2Id = Int32.Parse(subjectId);
                }
            }

            subject.AssignedStudentsCount += 1;

            await subjectService.UpdateAsync(subject);

            await userManager.UpdateAsync(user);

            return RedirectToAction("Index", "Home"); ;
        }
    }
}
