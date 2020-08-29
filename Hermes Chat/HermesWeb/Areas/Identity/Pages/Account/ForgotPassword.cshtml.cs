using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using HermesLogic.Interfaces;
using HermesModels.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HermesChat.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly IUserManager _userManager;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(IUserManager userManager, IEmailSender emailSender)
        {
            _emailSender = emailSender;
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(15)]
            [Display(Name = "User Name")]
            public string UserName { get; set; }
            [Required]
            [EmailAddress]
            public string Email { get; set; }

        }

       public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.GetUser(new string[] { Input.Email }, UserRetrieveOption.GetByEmail); //  change to asp net user
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, errorMessage: $"User name or user e-mail does not exist");
                }
                else
                {
                    var code = "stringtochangelater";/*await _userManager.GeneratePasswordResetTokenAsync(user);*/
                    var callbackUrl = Url.Page(
                        "/Account/ResetPassword",
                        pageHandler: null,
                        values: new { code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(
                        Input.Email,
                        "Reset Password",
                    $"To change your pasword <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>click here</a>.");
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }
            }

            return Page();
        }

        public string PasswordGenerator()
        {
            string lowCase = "abcdefghijklmnopqrstuvwxyz";
            string upCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string num = "0123456789";
            string spc = @"!#$%&*@\";
            string newPas = "";

            Random rng = new Random();
            StringBuilder build = new StringBuilder();
            for (int i = 0; i < 2; i++)
            {
                newPas = build.Append(lowCase[rng.Next(26)] + "" + upCase[rng.Next(26)] + "" + num[rng.Next(10)] + "" + spc[rng.Next(8)]).ToString();
            }

            return newPas;
        }
    }
}