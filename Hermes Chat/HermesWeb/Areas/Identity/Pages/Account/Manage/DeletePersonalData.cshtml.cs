using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using HermesLogic.Interfaces;
using HermesModels.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HermesChat.Areas.Identity.Pages.Account.Manage
{ 
    public class DeletePersonalDataModel : PageModel
    {
        private readonly IUserManager _userManager;

        public DeletePersonalDataModel(
            IUserManager userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public bool RequirePassword { get; set; }

        public async Task<IActionResult> OnGet()
        {
            //var user = _userManager.GetUser(HttpContext.User.ToString(), UserRetrieveOption.GetByDomainName);
            //if (user == null)
            //{
            //    return NotFound($"Unable to load user '{ HttpContext.User }'.");
            //}

            return Page();
        }

        public IActionResult OnPostAsync()
        {
            //var user = _userManager.GetUser(HttpContext.User.ToString(), UserRetrieveOption.GetByDomainName);
            //if (user == null)
            //{
            //    return NotFound($"Unable to load user with ID '{ HttpContext.User }'.");
            //}

            //if (_userManager.CredentialsManager.IsHashedPasswordEqual(Input.Password, user.Password))
            //{
            //    ModelState.AddModelError(string.Empty, "Password not correct.");
            //    return Page();
            //}

            //_userManager.DeleteUser(user.DomainName, user.UserName); ////////// COULD BE POTENTIAL ERROR, IF DOMAIN NAME WILL BE CHANGED< CHECK LATER !!!!!!

            return Redirect("~/");
        }
    }
}