using HermesLogic.Features.Authentication.Interfaces;
using HermesModels.MVC;
using HermesWeb.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HermesWeb.Controllers
{
    /// <summary>
    /// Controller for registration.
    /// </summary>
    [AllowAnonymous]
    public partial class RegistrationController : HermesApplicationController
    {
        /// <summary>
        /// Logic which contains all the authentication related logic.
        /// </summary>
        private readonly IAuthenticationLogic _authenticationLogic;

        /// <summary>
        /// Controller for registration.
        /// </summary>
        public RegistrationController(IAuthenticationLogic AAALogic)
        {
            _authenticationLogic = AAALogic;
        }

        /// <summary>
        /// Registration page.
        /// </summary>
        public virtual IActionResult Index()
        {
            // Disallow user to access registration functionalities if he is logged in already.
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(MVC.Chat.Index());
            }

            return View(MVC.Registration.Views.Register);
        }

        /// <summary>
        /// Tries to register user.
        /// </summary>
        /// <param name="registrationModel"></param>
        /// <returns>Returns view with completed registration if everything is ok.</returns>
        [HttpPost]
        [ModelStateFilter]
        [ValidateAntiForgeryToken]
        public virtual IActionResult Register(RegistrationModel registrationModel)
        {
            _authenticationLogic.RegisterUser(registrationModel);
            return View(MVC.Registration.Views.RegistrationConfirmed);
        }
    }
}