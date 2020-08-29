using HermesLogic.Interfaces;
using HermesModels.MVC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HermesChat.Controllers
{
    [AllowAnonymous]
    public class HomeController : ApplicationController
    {
        private readonly IAAALogic _AAALogic;

        public HomeController(IAAALogic AAALogic)
        {
            _AAALogic = AAALogic;
        }

        public IActionResult Index()
        {
            return View();
        }
 
        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            var isLogged = _AAALogic.LoginUser(model); 
            if (isLogged == true)
            {
                return RedirectToAction("Index", "Chat");
            }

            return View("error");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            _AAALogic.LogoutUser(CurrentUser);
            return Json(new { url = Url.Action("Index", "Home") });
        }
    }
}