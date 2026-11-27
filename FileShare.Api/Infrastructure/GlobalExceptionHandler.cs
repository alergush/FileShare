using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FileShare.Api.Infrastructure;

public sealed class GlobalExceptionHandler(
    IProblemDetailsService problemDetailsService,
    ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is BadHttpRequestException badRequestException)
        {
            logger.LogWarning("Bad request: {Message}", badRequestException.Message);

            return await WriteProblemAsync(
                httpContext,
                exception,
                badRequestException.StatusCode,
                "Bad request",
                "The request could not be read or its parameters are invalid.");
        }

        logger.LogError(exception, "Unhandled exception occurred");

        return await WriteProblemAsync(
            httpContext,
            exception,
            StatusCodes.Status500InternalServerError,
            "Server error",
            "An unexpected error occurred.");
    }

    private ValueTask<bool> WriteProblemAsync(
        HttpContext httpContext,
        Exception exception,
        int statusCode,
        string title,
        string detail)
    {
        httpContext.Response.StatusCode = statusCode;

        return problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail
            }
        });
    }
}
