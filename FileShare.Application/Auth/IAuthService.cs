using FileShare.Application.Auth.Dtos;
using FileShare.Domain.Common;

namespace FileShare.Application.Auth;

public interface IAuthService
{
    Task<Result<UserResponse>> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);
}
