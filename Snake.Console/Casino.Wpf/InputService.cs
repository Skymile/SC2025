using Casino.Domain;
using Casino.Services.Base;

namespace Casino.Wpf;

class InputService(IOutputService outputSerivce) : IInputService
{
    public Amount GetAmount()
    {
        return new Amount(0);
    }

    public Pocket GetPocketNumber()
    {
        return new Pocket(0, Color.Black);
    }
}
