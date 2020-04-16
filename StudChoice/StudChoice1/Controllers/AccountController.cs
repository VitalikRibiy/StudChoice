using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StudChoice.DAL.Models;
using StudChoice1.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace StudChoice.Controllers
{
    public class AccountController : Controller
    {
        public readonly UserManager<User> UserManager;

        private readonly ILogger<AccountController> logger;

        private readonly IConfiguration config;

        public AccountController(ILogger<AccountController> loggerVar, UserManager<User> userManagerVar, IConfiguration configVar)
        {
            logger = loggerVar;
            UserManager = userManagerVar;
            config = configVar;
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
                var name_surname = $"{model.Name} {model.Surname}";
                var user = new User { UserName = model.TransictionNumber, Email = model.Email, NormalizedUserName = name_surname };
                var randomGeneratedPassword = CreateRandomPassword();
                if (UserManager.FindByEmailAsync(model.Email) != null)
                {
                    ModelState.AddModelError(string.Empty, "There is an user with this email address");
                }

                var result = await UserManager.CreateAsync(user, randomGeneratedPassword);
                if (result.Succeeded)
                {
                    logger.LogInformation("User created a new account.");

                    SendEmail(model.Name, model.Surname, model.Email, model.TransictionNumber, randomGeneratedPassword);
                    return View("ToVerify");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View();                    
            }

            // If we got this far, something failed, redisplay form
            return View();
        }

        public void SendEmail(string name, string surname, string email, string transiction_code, string password)
        {
            MailAddress from = new MailAddress("studchoice.smtp@gmail.com", "StudChoice");

            var administrators = config.GetValue<string>("Administrators").Split(",");
            foreach (var admin in administrators)
            {
                MailAddress to = new MailAddress(admin);

                MailMessage m = new MailMessage(from, to);

                m.Subject = $"Verify User {email}";

                m.Body = $"<body>" +
                    $"<h2>Check if this user is real and confirm registration</h2>" +
                    $"<h3>Name: {name}</h3>" +
                    $"<h3>Surname: {surname}</h3>" +
                    $"<h3>Email: {email}</h3>" +
                    $"<h3>Password: {password}</h3>" +
                    $"<h3>Transiction Code: {transiction_code}</h3>" +
                    $"<a href='https://localhost:5001/Account/ConfirmEmailUser?email={email}&password={password}&transiction_code={transiction_code}'>VERIFY</a></body>";

                m.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.UseDefaultCredentials = false;

                smtp.Credentials = new NetworkCredential("studchoice.smtp@gmail.com", "studchoice123");
                smtp.EnableSsl = true;
                smtp.Send(m);
            }            
        }

        public ActionResult ConfirmEmailUser(string email, string password, string transiction_code)
        {
            MailAddress from = new MailAddress("studchoice.smtp@gmail.com", "StudChoice");
   
            MailAddress to = new MailAddress(email);

            MailMessage m = new MailMessage(from, to);

            m.Subject = $"Your account was verified!";

            m.Body = $"<body>" +
                $"<h2>You can login using this credentials or change password after successfull login.</h2>" +
                $"<h3>Transiction Code: {transiction_code}</h3>" +
                $"<h3>Password: {password}</h3>";
            m.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

            smtp.Credentials = new NetworkCredential("studchoice.smtp@gmail.com", "studchoice123");
            smtp.EnableSsl = true;
            smtp.Send(m);           
            
            return View();
        }

        private static string CreateRandomPassword(int length = 15)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new Random();
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }

            var password = new string(chars);
            password += random.Next(111, 1111);
            password += "_";
            return password;
        }
    }
}