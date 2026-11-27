using FluentValidation;

namespace FileShare.Api.Filters;

public sealed class ValidationFilter<TRequest> : IEndpointFilter
    where TRequest : class
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var validator = context.HttpContext.RequestServices
            .GetRequiredService<IValidator<TRequest>>();

        var request = context.Arguments.OfType<TRequest>().FirstOrDefault()
                      ?? throw new InvalidOperationException(
                          $"Endpoint has no argument of type {typeof(TRequest).Name}.");

        var validationResult = await validator.ValidateAsync(
            request,
            context.HttpContext.RequestAborted);

        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(validationResult.ToDictionary());
        }

        return await next(context);
    }
}
