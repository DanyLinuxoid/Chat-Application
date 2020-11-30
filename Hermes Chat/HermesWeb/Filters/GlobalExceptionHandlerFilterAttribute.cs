using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using System;

namespace HermesWeb.Filters
{
    public class GlobalExceptionHandlerFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IModelMetadataProvider _modelMetadataProvider;
        private readonly ILogger<GlobalExceptionHandlerFilterAttribute> _logger;

        public GlobalExceptionHandlerFilterAttribute(
            IModelMetadataProvider modelMetadataProvider,
            ILogger<GlobalExceptionHandlerFilterAttribute> logger)
        {
            _logger = logger;
            _modelMetadataProvider = modelMetadataProvider;
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError($"{context.Exception.Message}\nTrace:{context.Exception.StackTrace}");
            var result = new ViewResult { ViewName = "Error" };
            result.ViewData = new ViewDataDictionary(_modelMetadataProvider,
                                                        context.ModelState);
            result.ViewData.Add("Exception", context.Exception);
            context.Result = result;
        }
    }
}
