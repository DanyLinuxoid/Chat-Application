using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace HermesWeb.Filters
{
    /// <summary>
    /// Session filter, determines when user has session and when not, is global.
    /// </summary>
    public class SessionStateFilterGlobalAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Context accessor.
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Session filter, determines when user has session and when not, is global.
        /// </summary>
        public SessionStateFilterGlobalAttribute(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Before action gets excuted, check session.
        /// </summary>
        /// <param name="context">Execution context.</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            bool isAnonymous = context.ActionDescriptor.EndpointMetadata
                .Any(em => em.GetType() == typeof(AllowAnonymousAttribute));

            // If this is not anonymous controller where user is allowed to be freely - proceed to check.
            if (!isAnonymous)
            {
                string currentController = (string)context.RouteData.Values["Controller"];
                string currentAction = (string)context.RouteData.Values["Action"];

                // Check is session is ok and is not cleared, which happens on timeout.
                bool isUserIdInSession = _httpContextAccessor.HttpContext.Session.TryGetValue("AspNetUserId", out byte[] value);

                // If id is empty, then session is cleared - redirect to login page.
                if (!isUserIdInSession)
                {
                    // Return user to home page but with possibility to return if login will be successfull.
                    context.Result = new RedirectResult($"~/Home?ReturnUrl=" + currentController + "/" + currentAction);

                    // Also have to clear cookie.
                    _httpContextAccessor.HttpContext.SignOutAsync();
                }
            }
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // Empty...
        }
    }
}
