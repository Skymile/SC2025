using System.Windows.Input;

namespace Biometrics.ViewModels;

public class Command(Action<object?> action) : ICommand
{
    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => true;
    public void Execute(object? parameter) => action(parameter);
}
