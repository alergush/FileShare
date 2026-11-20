using FileShare.Application.Abstractions;

using Microsoft.AspNetCore.Identity;

namespace FileShare.Infrastructure.Security;

public sealed class PasswordHasher : IPasswordHasher
{
    private static readonly object NoUser = new();
    private readonly PasswordHasher<object> _hasher = new();

    public string Hash(string password) =>
        _hasher.HashPassword(NoUser, password);

    public bool Verify(string password, string passwordHash)
    {
        var result = _hasher.VerifyHashedPassword(NoUser, passwordHash, password);
        return result != PasswordVerificationResult.Failed;
    }
}
