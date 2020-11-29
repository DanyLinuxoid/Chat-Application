using HermesLogic.Base.UserManagement;
using HermesModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HermesWeb.Controllers
{
    /// <summary>
    /// Wrapper for base controller.
    /// </summary>
    [Authorize]
    public partial class HermesApplicationController : Controller
    {
        /// <summary>
        /// User management.
        /// </summary>
        protected IUserManager _userManager;

        /// <summary>
        /// Wrapper for base controller.
        /// </summary>
        public HermesApplicationController() 
        {
            /// DI is not working with T4MVC/R4MVC base classes normally, as it uses default constructor all the time
            /// https://stackoverflow.com/questions/15313666/cannot-inherit-a-base-controller-class-which-has-no-default-constructor
            /// Have to use service locator, yes, this is bad, but it's ok to be used only here.
            _userManager = DependencyInjector.GetService<IUserManager>();
        }

        /// <summary>
        /// Retrieves current user id.
        /// </summary>
        protected long CurrentUserId => _userManager.CurrentUserSessionValues.AspNetUserId;

        /// <summary>
        /// Simple object to return when everything is ok.
        /// </summary>
        /// <param name="obj">Object to wrap into result.</param>
        /// <returns></returns>
        protected OkObjectResult HermesSimpleOkResult(object obj = null)
        {
            if (obj == null)
            {
                obj = new { result = "ok" };
            }

            return new OkObjectResult(obj)
            {
                StatusCode = 200,
            };
        }
    }
}