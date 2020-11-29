using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HermesWeb.Filters
{
    /// <summary>
    /// Validation through action filter.
    /// </summary>
    public class ModelStateFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Before action is executed - validate model.
        /// </summary>
        /// <param name="context">Execution context.</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // If model is not valid - return errors.
            if (!context.ModelState.IsValid &&
                context.Controller is Controller controller)
            {
                context.Result = new ViewResult()
                {
                    ViewData = controller.ViewData,
                    TempData = controller.TempData
                };
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // Empty...
        }
    }
}