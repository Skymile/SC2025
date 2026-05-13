using System.ComponentModel;

namespace Keystrokes.Desktop;

public class MainViewModel : INotifyPropertyChanged
{
    public string MainText 
    { 
        get; 
        set 
        {
            field = value;
            PropertyChanged?.Invoke(this, new(nameof(MainText)));
        }
    } = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
}
