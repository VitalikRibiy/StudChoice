using Microsoft.AspNetCore.Mvc;

namespace StudChoice1.Controllers
{
   public class LoginController : Controller
    {
    //    public readonly UserManager<User> _userManager;
    //    public readonly SignInManager<User> _signInManager;
    //    [BindProperty]
    //    public InputModel Input { get; set; }

    //    [HttpGet]
    //    public ActionResult Login()
    //    {
    //        return View();
    //    }

    //    [HttpPost]
    //    public IActionResult Login(string returnUrl = null)
    //    {
    //        returnUrl = returnUrl ?? Url.Content("~/");

    //        if (ModelState.IsValid)
    //        {
                
    //            var user = new IdentityUser { UserName = Input.TransictionNumber };
    //            var resultReg =  _userManager.CreateAsync(user, Input.Password);
    //            if (resultReg.IsCompletedSuccessfully)
    //            {
    //                var result = _signInManager.PasswordSignInAsync(Input.TransictionNumber, Input.Password, Input.RememberMe, lockoutOnFailure: false);
    //                if (result.IsCompletedSuccessfully)
    //                {
    //                    //loginModel._logger.LogInformation("User logged in.");
    //                    return LocalRedirect(returnUrl);
    //                }
                   
    //                else
    //                {
    //                    //ModelState.AddModelError(string.Empty, "Invalid login attempt.");
    //                    return View();
    //                }
                  
    //            }
    //            else
    //            {
    //                var result = _signInManager.PasswordSignInAsync(Input.TransictionNumber, Input.Password, Input.RememberMe, lockoutOnFailure: false);
    //                if (result.IsCompletedSuccessfully)
    //                {
    //                    //loginModel._logger.LogInformation("User logged in.");
    //                    return LocalRedirect(returnUrl);
    //                }
                    
    //                else
    //                {
    //                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
    //                    return View();
    //                }
    //            }
                
    //        }

    //        // If we got this far, something failed, redisplay form
    //        return View();
    //    }
    }
}
