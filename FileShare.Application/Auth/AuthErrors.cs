using FileShare.Domain.Common;

namespace FileShare.Application.Auth;

public static class AuthErrors
{
    public static readonly Error EmailTaken = Error.Conflict(
        "auth.email_taken",
        "Email address is already in use.");
}
