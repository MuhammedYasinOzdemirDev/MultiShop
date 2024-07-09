using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MultiShop.Catalog.Handles;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var statusCode = (int)HttpStatusCode.InternalServerError;
        var errorDetails = new ErrorDetails()
        {
            StatusCode = statusCode,
            Message = context.Exception.Message
        };

        context.Result = new JsonResult(errorDetails)
        {
            StatusCode = statusCode
        };

        base.OnException(context);
    }
}