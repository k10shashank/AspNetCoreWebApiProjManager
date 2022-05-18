using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace AspNetCoreWebApiProjManager.Attributes
{
    public class ExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            Exception exception = context.Exception;
            string exceptionName = exception.GetType().Name;

            HttpStatusCode errorCode;
            string errorMessage;

            switch (exceptionName)
            {
                case "AppException":
                    string[] messages = exception.Message.Split(':');
                    errorCode = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), messages[0]);
                    errorMessage = messages[1];
                    break;
                default:
                    errorCode = HttpStatusCode.BadRequest;
                    errorMessage = "Some Error Occurred.";
                    break;
            }

            ErrorModel error = new(errorCode, errorMessage);

            context.Result = errorCode switch
            {
                HttpStatusCode.BadRequest => new BadRequestObjectResult(error),
                HttpStatusCode.Unauthorized => new UnauthorizedObjectResult(error),
                HttpStatusCode.NotFound => new NotFoundObjectResult(error),
                HttpStatusCode.Conflict => new ConflictObjectResult(error),
                _ => new BadRequestObjectResult(error),
            };
            base.OnException(context);
        }
    }

    public class ErrorModel
    {
        public ErrorModel(HttpStatusCode errorCode, string errorMessage)
        {
            ERROR_CODE = errorCode;
            ERROR_MSG = errorMessage;
        }
        public HttpStatusCode ERROR_CODE { get; set; }
        public string ERROR_MSG { get; set; }
    }
}
