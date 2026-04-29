using Casino.Domain;

namespace Casino.Services.Base;

public interface IInputService
{
    Pocket GetPocketNumber();
    Amount GetAmount();
}