using Biometrics.Validations.Base;

namespace Biometrics.Validations.Domain;

public readonly record struct FirstName(string Value)
{
    public static Validation<FirstName> TryCreate(string value) =>
        Validation.Validate(
            nameof(FirstName),
            () => new FirstName(value),

            string.IsNullOrWhiteSpace(value)
                ? Errors.FirstName_CannotBeNullOrWhitespace : null,

            value?.Length < 3
                ? Errors.FirstName_HasToBeAtLeast5Length : null
        );
}
