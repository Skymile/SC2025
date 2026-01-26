using Biometrics.Validations.Base;

namespace Biometrics.Validations.Domain;

public record User(
    UserId      UserId,
    FirstName   FirstName,
    CountryCode Country)
{
    public static Validation<User> TryCreate(
        string userId,
        string firstName,
        string country)
    {
        var idRes = UserId.TryCreate(userId);
        var firstNameRes = FirstName.TryCreate(firstName);
        var countryRes = CountryCodeModule.TryCreate(country);

        return Validation.Validate(
                nameof(User),
                () => new User(idRes.Value, firstNameRes.Value, countryRes.Value),
                string.Join(Environment.NewLine, idRes.Error?.Select(i => i.Message)!),
                string.Join(Environment.NewLine, firstNameRes.Error?.Select(i => i.Message)!),
                string.Join(Environment.NewLine, countryRes.Error?.Select(i => i.Message)!)
            );
    }
    
}
