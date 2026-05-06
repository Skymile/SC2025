using System.Windows.Input;

namespace Casino.Wpf;

public class Command : ICommand
{
    public Command(Action<object?> action) =>
        this.action = action;

    public Command(Action action) : this(_ => action()) { }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => true;
    public void Execute(object? parameter) =>
        action?.Invoke(parameter);

    private Action<object?> action;
}
