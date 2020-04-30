using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StudChoice.DAL.Models;
using StudChoice1.Models;
using System;
using System.Net;
using StudChoice.Models;
using System.Text.Encodings.Web;
using System.Web;
using System.Threading.Tasks;
using System.Net.Mail;

namespace StudChoice.Controllers
{
    public class AccountController : Controller
    {
        public RegisterModel Model { get; set; }
        readonly ChangePasswordModel ChangePasswordModel;
        public AccountModel AccountModel;
        public readonly UserManager<User> userManager;
        public readonly SignInManager<User> signInManager;
        private readonly ILogger<AccountController> logger;
        private readonly IConfiguration config;
        public AccountController(ILogger<AccountController> logger, UserManager<User> userManager, IConfiguration config, SignInManager<User> signInManager)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.config = config;
            this.signInManager = signInManager;
            ChangePasswordModel = new ChangePasswordModel();
        }

        [HttpGet]
        public ActionResult VerifyMe()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> VerifyMe(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var name_surname = $"{Model.Name} {Model.Surname}";
                var user = new User { UserName = Model.TransictionNumber, Email = Model.Email, NormalizedUserName = name_surname };
                var check_email = userManager.FindByEmailAsync(Model.Email);
                if (check_email.Result!= null)
                {
                    ModelState.AddModelError(string.Empty, "There is an user with this email address");
                    return View("VerifyMe");
                }
                //add checking if there is user with this transiction code
                var result = await userManager.CreateAsync(user, Model.Password);
                if (result.Succeeded)
                {
                    logger.LogInformation("User created a new account.");

                    SendEmail(Model.Name,Model.Surname,Model.Email,Model.TransictionNumber);
                    return View("ToVerify");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View();                    
            }            
            return View();
        }

        public void SendEmail(string name, string surname, string email,string transiction_code)
        {
            MailAddress from = new MailAddress("studchoice.smtp@gmail.com", "StudChoice");

            var administrators = config.GetValue<string>("Administrators").Split(",");
            foreach (var admin in administrators)
            {
                MailAddress to = new MailAddress(admin);

                MailMessage m = new MailMessage(from, to)
                {
                    Subject = $"Verify User {email}"
                };
                var http = HttpContext.Request.Scheme;
                var request = HttpContext.Request.Host.ToUriComponent();
                var url = http+ "://"+request + $"/Account/ConfirmEmailUser?email={email}";

                m.Body = $"<body>" +
                    $"<h2>Check if this user is real and confirm registration</h2>" +
                    $"<h3>Name: {name}</h3>" +
                    $"<h3>Surname: {surname}</h3>" +
                    $"<h3>Email: {email}</h3>" +                    
                    $"<h3>Transiction Code: {transiction_code}</h3>" +
                    $"<a href='{url}'>VERIFY</a></body>";

                m.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    UseDefaultCredentials = false,

                    Credentials = new NetworkCredential("studchoice.smtp@gmail.com", "studchoice123"),
                    EnableSsl = true
                };
                smtp.Send(m);
            }            
        }
        public ActionResult ConfirmEmailUser(string email)
        {
            var user = userManager.FindByEmailAsync(email);
            user.Result.EmailConfirmed = true;
            userManager.UpdateAsync(user.Result);

            MailAddress from = new MailAddress("studchoice.smtp@gmail.com", "StudChoice");
   
            MailAddress to = new MailAddress(email);

            MailMessage m = new MailMessage(from, to)
            {
                Subject = $"Your account was verified!"
            };
            
            var http = HttpContext.Request.Scheme;
            var request = HttpContext.Request.Host.ToUriComponent();
            var url = http + "://" + request + "/Home/Login";


            m.Body = $"<body>" +
                $"<h2>Hi {user.Result.NormalizedUserName}. Your account was verified by administrators so now you can login to <a href='{url}'>StudChoice</a>.</h2>";
            m.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("studchoice.smtp@gmail.com", "studchoice123"),
                EnableSsl = true
            };
            smtp.Send(m);           
            
            return View();
        }

        public ActionResult LogOut(string returnUrl = null)
        {
            signInManager.SignOutAsync();
            logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Home","Index");
            }
        }

        private async Task LoadAsync(IdentityUser<int> user)
        {
            var currentUser = await userManager.GetUserAsync(User);

            AccountModel = new AccountModel
            {
                Name = currentUser.NormalizedUserName,
                Email = currentUser.Email,
                TransictionCode = currentUser.UserName
            };
        }

        public async Task<ActionResult> Manage()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);

            return View("ManageIndex", AccountModel);
        }
        
        public ActionResult ChangePasswordAsync()
        {
            return View("ChangePassword", ChangePasswordModel);
        }


        [HttpPost]
        public async Task<ActionResult> ChangePasswordAsync(ChangePasswordModel changePasswordModel)
        {
            var currentUser = await userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                var checkPassword = userManager.CheckPasswordAsync(currentUser, changePasswordModel.CurrentPassword);
                if (await checkPassword)
                {
                    changePasswordModel.StatusMessage = "You changed your password successfully!";
                    await userManager.ChangePasswordAsync(currentUser, changePasswordModel.CurrentPassword, changePasswordModel.NewPassword);
                    return View("ChangePassword", changePasswordModel);
                }
                ModelState.AddModelError(string.Empty, "You entered wrong current password.");                
                return View("ChangePassword");
            }
            return View("ChangePassword");
        }
    }
}