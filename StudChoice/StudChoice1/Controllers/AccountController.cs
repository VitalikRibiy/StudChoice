using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StudChoice.DAL.Models;
using StudChoice1.Models;
using System;
using System.Net;
using Microsoft.Extensions.Configuration;
using StudChoice.Models;
using System.Text.Encodings.Web;
using System.Web;


namespace StudChoice.Controllers
{
    public class AccountController : Controller
    {
        public RegisterModel Model { get; set; }
        ChangePasswordModel ChangePasswordModel = new ChangePasswordModel();
        public AccountModel AccountModel;
        public readonly UserManager<IdentityUser<int>> _userManager;
        public readonly SignInManager<IdentityUser<int>> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _config;
        public AccountController(ILogger<AccountController> logger, UserManager<IdentityUser<int>> userManager, IConfiguration config, SignInManager<IdentityUser<int>> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _config = config;
            _signInManager = signInManager;
        }

        [BindProperty]
        public RegisterModel Model { get; set; }

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
                var user = new IdentityUser<int> { UserName = Model.TransictionNumber, Email = Model.Email, NormalizedUserName = name_surname };
                var check_email = _userManager.FindByEmailAsync(Model.Email);
                if (check_email.Result!= null)
                {
                    ModelState.AddModelError(string.Empty, "There is an user with this email address");
                    return View("VerifyMe");
                }
                //add checking if there is user with this transiction code
                var result = await _userManager.CreateAsync(user, Model.Password);
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

                MailMessage m = new MailMessage(from, to);

                m.Subject = $"Verify User {email}";
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

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.UseDefaultCredentials = false;

                smtp.Credentials = new NetworkCredential("studchoice.smtp@gmail.com", "studchoice123");
                smtp.EnableSsl = true;
                smtp.Send(m);
            }            
        }
        public ActionResult ConfirmEmailUser(string email)
        {
            var user = _userManager.FindByEmailAsync(email);
            user.Result.EmailConfirmed = true;
            _userManager.UpdateAsync(user.Result);

            MailAddress from = new MailAddress("studchoice.smtp@gmail.com", "StudChoice");
   
            MailAddress to = new MailAddress(email);

            MailMessage m = new MailMessage(from, to);

            m.Subject = $"Your account was verified!";

            var callbackUrl = Url.Page(
                    "/Home/Index",
                    values:null,
                    pageHandler: null,                    
                    protocol: Request.Scheme);
            
            var http = HttpContext.Request.Scheme;
            var request = HttpContext.Request.Host.ToUriComponent();
            var url = http + "://" + request + "/Home/Login";


            m.Body = $"<body>" +
                $"<h2>Hi {user.Result.NormalizedUserName}. Your account was verified by administrators so now you can login to <a href='{url}'>StudChoice</a>.</h2>";
            m.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

            smtp.Credentials = new NetworkCredential("studchoice.smtp@gmail.com", "studchoice123");
            smtp.EnableSsl = true;
            smtp.Send(m);           
            
            return View();
        }

        public ActionResult LogOut(string returnUrl = null)
        {
            _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
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
            var currentUser = await _userManager.GetUserAsync(User);

            AccountModel = new AccountModel
            {
                Name = currentUser.NormalizedUserName,
                Email = currentUser.Email,
                TransictionCode = currentUser.UserName
            };
        }

        public async Task<ActionResult> Manage()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);

            return View("ManageIndex", AccountModel);
        }
        
        public async Task<ActionResult> ChangePasswordAsync()
        {
            return View("ChangePassword", ChangePasswordModel);
        }


        [HttpPost]
        public async Task<ActionResult> ChangePasswordAsync(ChangePasswordModel changePasswordModel)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                var checkPassword = _userManager.CheckPasswordAsync(currentUser, changePasswordModel.CurrentPassword);
                if (await checkPassword)
                {
                    changePasswordModel.StatusMessage = "You changed your password successfully!";
                    await _userManager.ChangePasswordAsync(currentUser, changePasswordModel.CurrentPassword, changePasswordModel.NewPassword);
                    return View("ChangePassword", changePasswordModel);
                }
                ModelState.AddModelError(string.Empty, "You entered wrong current password.");                
                return View("ChangePassword");
            }
            return View("ChangePassword");
        }
    }
}