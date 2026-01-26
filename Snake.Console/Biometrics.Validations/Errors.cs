namespace Biometrics.Validations;

public static class Errors
{
    public const string UserId_CannotBeNullOrWhitespace =
        "UserId cannot be null or empty";
    public const string FirstName_CannotBeNullOrWhitespace =
        "FirstName cannot be null or empty";
    public const string UserId_HasToBeAtLeast5Length =
        "UserId must be at least 5 characters long";
    public const string FirstName_HasToBeAtLeast5Length =
        "FirstName must be at least 3 characters long";
    public const string CountryCode_IsInvalid =
        "CountryCode is invalid";
    public const string CountryCode_IsNullOrWhitespace =
        "CountryCode cannot be null or empty";
}
