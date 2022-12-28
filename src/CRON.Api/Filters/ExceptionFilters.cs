using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace CRON.Api.Filters;
public class ExceptionFilters : IExceptionFilter
{
    // to avoid unexpected exceptions
    public void OnException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult("Internal server error.");
    }
}
