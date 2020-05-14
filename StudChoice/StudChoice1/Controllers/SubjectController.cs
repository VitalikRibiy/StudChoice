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

        public async Task<IActionResult> Subjects(string subjectType)
        {
            var subjectDTOs = (await subjectService.GetAllAsync()).ToList();

            var user = await userManager.GetUserAsync(User);

            subjectDTOs = subjectDTOs.Where(x => x.Type == subjectType && x.Course == user.Course).ToList();

            return View("Subjects", subjectDTOs);
        }

        public async Task<IActionResult> View(string subjectId)
        {
            var subject = await subjectService.GetAsync(Int32.Parse(subjectId));

            return View("ViewSubject", subject);
        }

      

        public async Task<IActionResult> Choose(string subjectId, string term)
        {
           // await subjectService.updateState(Int32.Parse(subjectId));
            var user = await userManager.GetUserAsync(User);

            var subject = await subjectService.GetAsync(Int32.Parse(subjectId));

            int termNumber = Int32.Parse(term);
           
            
           

            if (termNumber == 1)
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
            else if(termNumber == 2)
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

           await userManager.UpdateAsync(user);

            return RedirectToAction("Index", "Home"); ;
        }
    }
}
