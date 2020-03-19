using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        

    public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
    }
}
