using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

using Biometrics.Core;
using Biometrics.ImageProcessing;

namespace Biometrics.ViewModels;

public class MainWindowVM : INotifyPropertyChanged
{
    public MainWindowVM()
    {
        fileToPath = Directory.EnumerateFiles("../../../Samples/")
            .ToDictionary(
                i => Path.GetFileNameWithoutExtension(i)
                    ?? throw new NullReferenceException(),
                i => i
            );

        Files = [.. Directory.EnumerateFiles("../../../Samples/")
                .Select(Path.GetFileNameWithoutExtension)
                .OfType<string>()];

        SelectedFile = Files[0];
        SelectedWindow = Windows[0];
    }

    public IImage GetImage() => new Image(fileToPath[SelectedFile])
        .Apply(new Grayscale())
        .Apply(new MedianFilter())
        .Apply(new MedianFilter())
        .Apply(new ConvolutionFilter(windowDict[SelectedWindow])
        {
            Sensitivity = 1.0
        })
        ;

    private void Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        field = value;
        PropertyChanged?.Invoke(this, new(propertyName));
    }

    public string SelectedFile   { get => field; set => Set(ref field, value); }
    public string SelectedWindow { get => field; set => Set(ref field, value); }
    public string[] Files        { get => field; set => Set(ref field, value); }
    public string[] Windows      { get => field; set => Set(ref field, value); } = windowDict.Keys.ToArray();

    private static readonly Dictionary<string, double[]> windowDict = typeof(Window3x3)
        .GetFields(BindingFlags.Public | BindingFlags.Static)
        .ToDictionary(i => i.Name, i => (double[])i.GetValue(null)!);

    private readonly Dictionary<string, string> fileToPath = [];

    public event PropertyChangedEventHandler? PropertyChanged;
}
