using Book.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Book.Server.Filters;

public class ApiExceptionFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var msg = "internal server error";

        if (context.Exception is ValidationException)
        {
            msg = context.Exception.Message;
        }
        
        context.Result = new BadRequestObjectResult(new { Message = msg });
    }
}
