using System;
using System.Diagnostics;
using System.Threading.Tasks;
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
        public readonly UserManager<IdentityUser<int>> _userManager;
        public readonly SignInManager<IdentityUser<int>> _signInManager;
        public ISubjectService subjectService;
        [BindProperty]
        public InputModel Input { get; set; }

        

    public HomeController(ILogger<HomeController> logger, ISubjectService subj, UserManager<IdentityUser<int>> userManager, SignInManager<IdentityUser<int>> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
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
        public async Task<IActionResult> LoginAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(Input.TransictionNumber);
                var result = await _signInManager.PasswordSignInAsync(Input.TransictionNumber, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    try
                    {
                        await _signInManager.SignInAsync(user, false);
                        return LocalRedirect(returnUrl);
                    }
                    catch
                    {
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");

            }
            return View();
        }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult Subject()
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
