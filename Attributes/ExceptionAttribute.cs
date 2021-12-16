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

            ErrorModel error = new ErrorModel(errorCode, errorMessage);

            switch (errorCode)
            {
                case HttpStatusCode.BadRequest:
                    context.Result = new BadRequestObjectResult(error);
                    break;
                case HttpStatusCode.Unauthorized:
                    context.Result = new UnauthorizedObjectResult(error);
                    break;
                case HttpStatusCode.NotFound:
                    context.Result = new NotFoundObjectResult(error);
                    break;
                case HttpStatusCode.Conflict:
                    context.Result = new ConflictObjectResult(error);
                    break;
                default:
                    context.Result = new BadRequestObjectResult(error);
                    break;
            }

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
