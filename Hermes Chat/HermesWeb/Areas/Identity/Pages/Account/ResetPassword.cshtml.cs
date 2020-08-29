using System.ComponentModel.DataAnnotations;
using HermesLogic.Interfaces;
using HermesModels.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HermesChat.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordModel : PageModel
    {
        private readonly IUserManager _userManager;

        public ResetPasswordModel(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "User name")]
            public string UserName { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public string Code { get; set; }
        }

        public IActionResult OnGet(string code = null)
        {
            if (code == null)
            {
                return BadRequest("A code must be supplied for password reset.");
            }
            else
            {
                Input = new InputModel
                {
                    Code = code
                };

                return Page();
            }
        }

        public IActionResult OnPostAsync()// TODO MUST BE ASYNC, JUST LIKE ALL OTHERS
        {
            //if (!ModelState.IsValid || Input.Password != Input.ConfirmPassword)
            //{
            //    return Page();
            //}

            //var user = _userManager.GetUser(Input.UserName, UserRetrieveOption.GetByUsername);
            //if (user == null || _userManager.CredentialsManager.IsHashedPasswordEqual(Input.Password, user.Password))
            //{
            //    // Don't reveal that the user does not exist or provided password is incorrect
            //    return RedirectToPage("./ResetPasswordConfirmation");
            //}

            //_userManager.UpdateUserInformation(user, 
            //    new { Password = _userManager.CredentialsManager.GetUserPasswordInHashedFormat(Input.Password) }); // VALIDATION, THAT UPDATE WAS SUCCESSFULL IS NEEDED TODO

            return RedirectToPage("./ResetPasswordConfirmation"); // REDIRECT ONLY IF SUCCESSFULL TODO

            //foreach (var error in result.Errors)
            //{
            //    ModelState.AddModelError(string.Empty, error.Description); // THIS IS NEEDED!!!!!!!!!!!!!!!!!
            //}
        }
    }
}