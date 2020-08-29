using HermesLogic.Interfaces;
using HermesModels.MVC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HermesChat.Controllers
{
    [AllowAnonymous]
    public class RegistrationController : ApplicationController
    {
        private readonly IAAALogic _AAALogic;

        public RegistrationController(IAAALogic AAALogic)
        {
            _AAALogic = AAALogic;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegistrationModel registrationModel)
        {
            var isRegistered = _AAALogic.RegisterUser(registrationModel);
            if (isRegistered == true)
            {
                return View("RegistrationConfirmed");
            }
            // ERROR PAGE IF NULL
            return View("Index");
        }
    }
}