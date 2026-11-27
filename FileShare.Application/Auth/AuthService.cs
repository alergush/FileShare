using FileShare.Application.Abstractions;
using FileShare.Application.Abstractions.Persistence;
using FileShare.Application.Auth.Dtos;
using FileShare.Domain.Common;
using FileShare.Domain.Constants;
using FileShare.Domain.Entities;
using FileShare.Domain.Enums;

namespace FileShare.Application.Auth;

public sealed class AuthService(
    IUserRepository userRepository,
    ISubscriptionRepository subscriptionRepository,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork) : IAuthService
{
    public async Task<Result<UserResponse>> RegisterAsync(
        RegisterRequest request,
        CancellationToken cancellationToken = default)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        if (await userRepository.ExistsByEmailAsync(email, cancellationToken))
        {
            return AuthErrors.EmailTaken;
        }

        var freeTier = await subscriptionRepository.GetTierByNameAsync(
                           SubscriptionTierNames.Free, cancellationToken)
                       ?? throw new InvalidOperationException("The Free subscription tier is missing.");

        var user = new User
        {
            Email = email,
            PasswordHash = passwordHasher.Hash(request.Password),
            FullName = string.IsNullOrWhiteSpace(request.FullName) ? null : request.FullName.Trim(),
            Role = UserRole.User
        };

        var subscription = new UserSubscription
        {
            User = user,
            SubscriptionTierId = freeTier.Id,
            StartDate = DateTime.UtcNow,
            Status = SubscriptionStatus.Active
        };

        await userRepository.AddAsync(user, cancellationToken);
        await subscriptionRepository.AddAsync(subscription, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new UserResponse(user.Email, user.FullName);
    }
}
