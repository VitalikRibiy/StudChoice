using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudChoice.BLL.DTOs;
using StudChoice.BLL.Services.Interfaces;
using StudChoice.DAL.Models;
using StudChoice.Models;
using StudChoice1.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace StudChoice.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ISubjectService subjectService;
        private readonly IFacultyService facultyService;
        private readonly IProfessorService professorService;
        private readonly ICathedraService cathedraService;
        private readonly ILogger<HomeController> logger;
        private readonly IMapper mapper;

        public HomeController(
            ILogger<HomeController> loggerVar,
            ISubjectService subjectServiceVar,
            IFacultyService facultyServiceVar,
            IProfessorService professorServiceVar,
            ICathedraService cathedraServiceVar,
            UserManager<User> userManagerVar,
            SignInManager<User> signInManagerVar,
            IMapper mapperVar)
        {
            logger = loggerVar;
            userManager = userManagerVar;
            signInManager = signInManagerVar;
            subjectService = subjectServiceVar;
            cathedraService = cathedraServiceVar;
            facultyService = facultyServiceVar;
            professorService = professorServiceVar;
            mapper = mapperVar;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public async Task<IActionResult> Index()
        {
            var user = mapper.Map<UserDTO>((await userManager.GetUserAsync(User)));

            if (user != null)
            {

                user.FacultyName = (await facultyService.GetAsync(user.FacultyId)).DisplayName;

                user.CathedraName = (await cathedraService.GetAsync(user.CathedraId)).DisplayName;

                if (user.Dv1Id != null) user.Dv1IName = (await subjectService.GetAsync((long)user.Dv1Id)).Name;

                if (user.Dv2Id != null) user.Dv2IName = (await subjectService.GetAsync((long)user.Dv2Id)).Name;

                if (user.Dvvs1Id != null) user.Dvvs1Name = (await subjectService.GetAsync((long)user.Dvvs1Id)).Name;

                if (user.Dvvs2Id != null) user.Dvvs2Name = (await subjectService.GetAsync((long)user.Dvvs2Id)).Name;

                return View("Index", user);
            } else
            {
                return View();
            }

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
                var user = await userManager.FindByNameAsync(Input.TransictionNumber);
                if (user != null)
                {
                    if (!user.EmailConfirmed)
                    {
                        ModelState.AddModelError(string.Empty, "Your account is not verified. You will be able to login after administrator confirm you.");
                        return View();
                    }
                }
                var result = await signInManager.PasswordSignInAsync(Input.TransictionNumber, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    try
                    {
                        await signInManager.SignInAsync(user, false);
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

        protected override void Dispose(bool disposing)
        {
            subjectService.Dispose();
            base.Dispose(disposing);
        }
    }
}
