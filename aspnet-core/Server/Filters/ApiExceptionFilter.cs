using Book.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;

namespace Book.Server.Filters;

public class ApiExceptionFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var response = new ResponseModel<object>(false);
        var validationErrors = new List<ValidationResult> { new ValidationResult("internal server error") };

        if (context.Exception is Shared.Exceptions.ValidationException msg)
        {
            validationErrors = msg.ValidationErrors;
        }

        response.Errors = validationErrors;
        context.Result = new BadRequestObjectResult(response);
    }
}
