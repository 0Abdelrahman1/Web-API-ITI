using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Project.Filters
{
    public class HandleExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            ContentResult res = new ContentResult();
            res.StatusCode = 500;
            res.Content = $"Error Happend : {context.ActionDescriptor.DisplayName} , {context.Exception.Message}";
            context.Result = res;
        }
    }
}
