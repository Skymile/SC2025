using System.Globalization;
using System.Numerics;

using Casino.Domain;
using Casino.Services.Base;

namespace Casino.Services;

public class InputService(IOutputService Output) : IInputService
{
    public Amount GetAmount() => 
        GetDomainResult<Amount, decimal>(Amount.TryCreate,
            Text.TypeAmount.Format(Amount.MinAmount, Amount.MaxAmount)
        );

    public Pocket GetPocketNumber() => 
        GetDomainResult<Pocket, int>(Pocket.TryCreate,
            Text.TypePocketNumber.Format(Pocket.MinPocket, Pocket.MaxPocket)
        );

    private T GetDomainResult<T, TNumber>(
            Func<TNumber, Result<T>> creator,
            string notification
        )
        where TNumber : INumber<TNumber>
    {
        Result<T> result;

        while (true)
        {
            Output.WriteMessage(notification);

            if (!TNumber.TryParse(
                    Console.ReadLine(), 
                    CultureInfo.InvariantCulture.NumberFormat, 
                    out var number
                ))
                continue;
            result = creator(number);

            if (!result.IsError)
                break;
            Output.WriteMessage(result.Error);
        }

        return result.Value;
    }
}
