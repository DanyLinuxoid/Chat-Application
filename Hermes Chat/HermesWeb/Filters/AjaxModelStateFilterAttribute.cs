using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace HermesWeb.Filters
{
    /// <summary>
    /// Model validation for ajax, which returns json like formatted errors.
    /// </summary>
    public class AjaxModelStateFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Before action executes - validate model.
        /// </summary>
        /// <param name="context">Execution context.</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // If not valid - format errors to key(field)/value(errors) and return object.
            if (!(context.Controller as Controller).ViewData.ModelState.IsValid) 
            {
                context.Result = new JsonResult(new
                {
                    errors = context.ModelState
                        .Where(x => x.Value.Errors.Any())
                        .Select(x => (x.Key, x.Value.Errors.Select(y => y.ErrorMessage).ToList()))
                });
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // Empty...
        }
    }
}