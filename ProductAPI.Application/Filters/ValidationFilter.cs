using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.GetErrors();
            context.Result = new BadRequestObjectResult(new { Errors = errors });
        }
    }
    public void OnActionExecuted(ActionExecutedContext context) { }
}