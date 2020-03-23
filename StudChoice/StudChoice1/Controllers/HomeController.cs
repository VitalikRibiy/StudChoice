using System.Collections.Generic;
using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudChoice.BLL.DTOs;
using StudChoice.BLL.Services.Interfaces;
using StudChoice1.Models;

namespace StudChoice1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public readonly UserManager<IdentityUser> _userManager;
        public readonly SignInManager<IdentityUser> _signInManager;
        [BindProperty]
        public InputModel Input { get; set; }

        ISubjectService subjectService;

    public HomeController(ILogger<HomeController> logger, ISubjectService subj)
        {
            _logger = logger;
            subjectService = subj;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {


                var user = new IdentityUser { UserName = Input.TransictionNumber };
                var resultReg = _userManager.CreateAsync(user, Input.Password);
                if (resultReg.IsCompletedSuccessfully)
                {
                    var result = _signInManager.PasswordSignInAsync(Input.TransictionNumber, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                    if (result.IsCompletedSuccessfully)
                    {
                        //loginModel._logger.LogInformation("User logged in.");
                        return View("Index");
                    }

                    else
                    {
                        //ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return View();
                    }

                }
                else
                {
                    var result = _signInManager.PasswordSignInAsync(Input.TransictionNumber, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                    if (result.IsCompletedSuccessfully)
                    {
                        //loginModel._logger.LogInformation("User logged in.");
                        return LocalRedirect(returnUrl);
                    }

                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return View();
                    }
                }

            }

            // If we got this far, something failed, redisplay form
            return View();
        }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult Subjects()
        {
            IEnumerable<SubjectDTO> subjectDTO = subjectService.GetSubjects();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SubjectDTO, SubjectViewModel>()).CreateMapper();
            var subjects = mapper.Map<IEnumerable<SubjectDTO>, List<SubjectViewModel>>(subjectDTO);
            return View(subjects);
        }

        protected override void Dispose(bool disposing)
        {
            subjectService.Dispose();
            base.Dispose(disposing);
        }
    }
}
