using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace Student.Service.Filters
{

    /// <summary>
    /// Nice approach to include exception handing into request pipeline
    /// Lifted from https://github.com/JasonGT/NorthwindTraders
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new Microsoft.AspNetCore.Mvc.JsonResult(new
            {
                error = new[] { context.Exception.Message, context.Exception.InnerException?.Message },
                stackTrace = context.Exception.StackTrace
            });
        }
    }

}
