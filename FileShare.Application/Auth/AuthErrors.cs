using FileShare.Domain.Common;

namespace FileShare.Application.Auth;

public class AuthErrors
{
    public static readonly Error InvalidEmail = Error.Validation(
        "auth.invalid_email",
        "Email address is not valid.");

    public static readonly Error PasswordTooShort = Error.Validation(
        "auth.password_too_short",
        "Password must be at least 8 characters long.");
}
