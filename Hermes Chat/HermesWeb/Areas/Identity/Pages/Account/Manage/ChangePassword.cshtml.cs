using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using HermesLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HermesChat.Areas.Identity.Pages.Account.Manage
{
    public class ChangePasswordModel : PageModel
    {
        private readonly IUserManager _userManager;
        public ChangePasswordModel(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Current password")]
            public string OldPassword { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            //var user = await _userManager.GetUserAsync(User);
            //if (user == null)
            //{
            //    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            //}

            //var hasPassword = await _userManager.HasPasswordAsync(user);
            //if (!hasPassword)
            //{
            //    return RedirectToPage("./SetPassword");
            //}

            return Page();
        }

        public IActionResult OnPostAsync() // MUST BE ASYNC TODO
        {
            if (!ModelState.IsValid || Input.NewPassword != Input.ConfirmPassword)
            {
                return Page();
            }

            //var user = _userManager.GetUser(new string[] { HttpContext.User.ToString() }, UserRetrieveOption.GetByDomainName);
            //if (user == null)
            //{
            //    return NotFound($"Unable to load user { HttpContext.User }.");
            //}

            //_userManager.UpdateUserInformation(user, new { Password = Input.NewPassword });

            //await _signInManager.RefreshSignInAsync(user); TODO - THIS IS NEEDED TO REFRESH COOKIES AND UPDATE PRINCIPAL/CLAIMS

            return RedirectToPage();
        }
    }
}