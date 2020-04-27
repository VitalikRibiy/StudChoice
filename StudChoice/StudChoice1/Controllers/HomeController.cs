﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudChoice.BLL.DTOs;
using StudChoice.BLL.Services.Interfaces;
using StudChoice.DAL.Models;
using StudChoice.Models;
using StudChoice1.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace StudChoice.Controllers
{
    public class HomeController : Controller
    {
        public readonly UserManager<User> UserManager;
        public readonly SignInManager<User> SignInManager;
        public ISubjectService SubjectService;
        private readonly ILogger<HomeController> logger;   
       
    public HomeController(ILogger<HomeController> loggerVar, ISubjectService subjVar, UserManager<User> userManagerVar, SignInManager<User> signInManagerVar)
        {
            logger = loggerVar;
            UserManager = userManagerVar;
            SignInManager = signInManagerVar;
            SubjectService = subjVar;
        }

        [BindProperty]
        public InputModel Input { get; set; }

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
                if (user != null)
                {
                    if (!user.EmailConfirmed)
                    {
                        ModelState.AddModelError(string.Empty, "Your account is not verified. You will be able to login after administrator confirm you.");
                        return View();
                    }
                }
                var result = await _signInManager.PasswordSignInAsync(Input.TransictionNumber, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    try
                    {
                        await SignInManager.SignInAsync(user, false);
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
            SubjectService.Dispose();
            base.Dispose(disposing);
        }
    }
}
