using Biometrics.Validations.Base;

namespace Biometrics.Validations.Domain;

public enum CountryCode { PL, UK, US, DE, FR, ES }

public static class CountryCodeModule
{
    public static Validation<CountryCode> TryCreate(string value) =>
        Validation.Validate(
            nameof(CountryCode),
            () => Enum.Parse<CountryCode>(value),

            string.IsNullOrWhiteSpace(value)
                ? Errors.CountryCode_IsNullOrWhitespace : null,

            !Enum.TryParse<CountryCode>(value, out _)
                ? Errors.CountryCode_IsInvalid : null
        );
}
