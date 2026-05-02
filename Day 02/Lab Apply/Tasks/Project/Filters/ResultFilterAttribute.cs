using Microsoft.AspNetCore.Mvc.Filters;

namespace Project.Filters
{
    public class ResultFilterAttribute : Microsoft.AspNetCore.Mvc.Filters.ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine($"OnResultExecuting of {context.ActionDescriptor.DisplayName}");
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine($"OnResultExecuted of {context.ActionDescriptor.DisplayName}");
        }
    }
}
