using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ExceptionFilterAttribute = Microsoft.AspNetCore.Mvc.Filters.ExceptionFilterAttribute;

namespace SafeboardSnake.WebApi.ExceptionFilters
{
    public class ArgumentExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public Type ExceptionType { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            var actionName = context.ActionDescriptor.DisplayName;
            var exceptionStack = context.Exception.StackTrace;
            var exceptionMessage = context.Exception.Message;

            context.Result = new ContentResult
            {
                Content = $"In method {actionName} exception thrown: \n {exceptionMessage} \n {exceptionStack}",
                StatusCode = (int?) HttpStatusCode.BadRequest
            };

            context.ExceptionHandled = true;

            return Task.CompletedTask;
        }
    }
}
