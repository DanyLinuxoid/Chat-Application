using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using HermesLogic.Interfaces;
using HermesModels.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HermesChat.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly IUserManager _userManager;

        public IndexModel(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        public IActionResult OnPostAsync() // TODO MUST BE ASYNC
        {
            if (ModelState.IsValid)
            {
                //var user = _userManager.GetUser(Input.Email, UserRetrieveOption.GetByEmail);
                //if (user != null)
                //{
                //    ModelState.AddModelError(string.Empty, errorMessage: $"Email '{Input.Email}' is already taken");
                //    return Page();
                //}
                //else
                //{
                //    return RedirectToPage();
                //}
            }

            return Page();
        }
    }
}