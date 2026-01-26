using Biometrics.Validations.Base;

namespace Biometrics.Validations.Domain;

public readonly record struct UserId(string Value)
{
    public static Validation<UserId> TryCreate(string value) =>
         Validation.Validate(
            nameof(UserId),
            () => new UserId(value),

            string.IsNullOrWhiteSpace(value)
                ? Errors.UserId_CannotBeNullOrWhitespace : null,

            value?.Length < 5
                ? Errors.UserId_HasToBeAtLeast5Length : null
        );
}
