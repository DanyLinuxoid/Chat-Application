using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HermesChat.Controllers
{
    public class ApplicationController : Controller
    {
        protected string CurrentUser
        {
            get
            {
                return HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Username")?.Value;
            }
        }
    }
}