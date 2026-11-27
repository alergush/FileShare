using FileShare.Api.Filters;
using FileShare.Api.Infrastructure;
using FileShare.Application.Auth;
using FileShare.Application.Auth.Dtos;

using Microsoft.AspNetCore.Http.HttpResults;

namespace FileShare.Api.Endpoints;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth")
            .WithTags("Auth");

        group.MapPost("/register", RegisterAsync)
            .WithSummary("Registers a new user with the Free subscription.")
            .WithValidation<RegisterRequest>()
            .ProducesProblem(StatusCodes.Status409Conflict);

        return app;
    }

    private static async Task<Results<Created<UserResponse>, ProblemHttpResult>> RegisterAsync(
        RegisterRequest request,
        IAuthService authService,
        CancellationToken cancellationToken)
    {
        var result = await authService.RegisterAsync(request, cancellationToken);

        if (result.IsFailure)
        {
            return result.ToProblem();
        }

        return TypedResults.Created("/api/auth/me", result.Value);
    }
}
