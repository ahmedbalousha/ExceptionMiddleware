using ExceptionMiddleware.API.Exceptions;
using System.Net;

namespace ExceptionMiddleware.API.Middlewares
{


    public class ExceptionHandellerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandellerMiddleware> _logger;

        public ExceptionHandellerMiddleware(RequestDelegate next, ILogger<ExceptionHandellerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                // Handle exception and return response
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode statusCode;
            string message;

            // Custom logic to handle specific exceptions
            switch (exception)
            {
                case OperationLoginFailedException _:
                    statusCode = HttpStatusCode.Unauthorized;
                    message = "Login failed. Invalid credentials.";
                    break;
                case OperationFailedException _:
                    statusCode = HttpStatusCode.BadRequest;
                    message = "The operation failed.";
                    break;
                case DuplicateEmailOrPhoneException _:
                    statusCode = HttpStatusCode.Conflict;
                    message = "Email or phone number already exists.";
                    break;
                case EntityNotFoundException _:
                    statusCode = HttpStatusCode.NotFound;
                    message = "The requested entity was not found.";
                    break;
                
                case DuplicateItemException _:
                    statusCode = HttpStatusCode.Conflict;
                    message = "DuplicateItemException.";
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    message = "An unexpected error occurred.";
                    break;
            }

            // Log the exception for debugging purposes
            _logger.LogError(exception, "Unhandled exception occurred");

            // Set the response status code and message
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var response = new
            {
                StatusCode = (int)statusCode,
                Message = message
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
