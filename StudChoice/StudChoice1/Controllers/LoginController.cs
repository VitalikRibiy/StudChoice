using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudChoice1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudChoice1.Controllers
{
   public class LoginController : Controller
    {
        public readonly UserManager<IdentityUser> _userManager;
        public readonly SignInManager<IdentityUser> _signInManager;
        [BindProperty]
        public InputModel Input { get; set; }

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
                var resultReg =  _userManager.CreateAsync(user, Input.Password);
                if (resultReg.IsCompletedSuccessfully)
                {
                    var result = _signInManager.PasswordSignInAsync(Input.TransictionNumber, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                    if (result.IsCompletedSuccessfully)
                    {
                        //loginModel._logger.LogInformation("User logged in.");
                        return LocalRedirect(returnUrl);
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
    }
}
