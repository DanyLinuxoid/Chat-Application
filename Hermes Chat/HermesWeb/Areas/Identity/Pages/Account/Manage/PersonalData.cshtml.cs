using HermesLogic.Interfaces;
using HermesModels.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;// //////////// WHAT IS tHIS LOGGER?? TODO

namespace HermesChat.Areas.Identity.Pages.Account.Manage
{
    public class PersonalDataModel : PageModel
    {
        private readonly IUserManager _userManager;

        public PersonalDataModel(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public IActionResult OnGet() // AWAIT, MUST BE ASYNC, TODO
        {
            //var user = _userManager.GetUser(HttpContext.User.ToString(), UserRetrieveOption.GetByDomainName);
            //if (user == null)
            //{
            //    return NotFound($"Unable to load user with ID '{ HttpContext.User }'.");
            //}

            return Page();
        }
    }
}