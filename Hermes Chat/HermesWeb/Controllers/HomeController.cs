using HermesLogic.Features.Authentication.Interfaces;
using HermesModels.MVC;
using HermesModels.User;
using HermesWeb.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HermesWeb.Controllers
{
    /// <summary>
    /// Home - Login page.
    /// </summary>
    [AllowAnonymous]
    public partial class HomeController : HermesApplicationController
    {
        /// <summary>
        /// Authentication logic after login.
        /// </summary>
        private readonly IAuthenticationLogic _authenticationLogic;

        /// <summary>
        /// Used to get value from view after logic, as after logic user is null in controller, not in view.
        /// This gets used for navigation.
        /// </summary>
        /// <returns>Chat user representation.</returns>
        public virtual ChatUser CurrentUser => _userManager.CurrentUser;

        /// <summary>
        /// Home - Login page.
        /// </summary>
        public HomeController(IAuthenticationLogic AAALogic)
        {
            _authenticationLogic = AAALogic;
        }

        /// <summary>
        /// Main login page.
        /// </summary>
        public virtual IActionResult Index()
        {
            // Disallow user to access login functionalities if he is logged in already
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(MVC.Chat.Index());
            }

            return View(MVC.Home.Views.Login);
        }

        /// <summary>
        /// Tries to log in user.
        /// </summary>
        /// <param name="model">Login model.</param>
        [HttpPost]
        [ModelStateFilter]
        public virtual IActionResult Login(LoginModel model)
        {
            _authenticationLogic.LoginUser(model);
            return RedirectToAction(MVC.Chat.Index());
        }

        [HttpPost]
        public virtual IActionResult Logout()
        {
            _authenticationLogic.LogoutUser();
            return Json(new { url = Url.RouteUrl(MVC.Home.Index()) });
        }
    }
}