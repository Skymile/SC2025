using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using Biometrics.Core;
using Biometrics.ImageProcessing;

namespace Biometrics.ViewModels;

public class MainWindowVM : INotifyPropertyChanged
{
    public MainWindowVM(IAlgorithmService algoService)
    {
        const string path = "../../../Samples/";
        this.algoService = algoService;
        fileToPath = algoService.GetFileToPathMap(path);
        Files = algoService.GetFilenames(path);
        Algorithms = algoService.GetAlgorithmNames();

        SelectionChanged = new Command(_ => RefreshImage?.Invoke());
        WindowSelectionChanged = new Command(_ => RefreshImage?.Invoke());

        var windowDict = algoService.GetAlgorithmWindows();
        Windows = windowDict.Keys.ToArray();

        SelectedAlgorithm = Algorithms[0];
        SelectedFile = Files[0];
        SelectedWindow = Windows[0];
    }

    public Action? RefreshImage { get; set; }

    public ICommand SelectionChanged { get; set; }
    public ICommand WindowSelectionChanged { get; set; }

    public IImage GetImage() => new Image(fileToPath[SelectedFile])
        .Apply(new Grayscale())
        .Apply(new MedianFilter())
        .Apply(new MedianFilter())
        .Apply(new ConvolutionFilter(algoService.GetAlgorithmWindows()[SelectedWindow])
        {
            Sensitivity = 1.0
        })
        ;

    public string SelectedFile   { get => field; set => Set(ref field, value); }
    public string SelectedWindow { get => field; set => Set(ref field, value); }
    public string[] Files        { get => field; set => Set(ref field, value); }
    public string[] Windows      { get => field; set => Set(ref field, value); }

    public string[] Algorithms { get => field; set => Set(ref field, value); }
    public string SelectedAlgorithm { get; set; }

    private readonly IAlgorithmService algoService;
    private readonly Dictionary<string, string> fileToPath = [];

    public event PropertyChangedEventHandler? PropertyChanged;

    private void Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        field = value;
        PropertyChanged?.Invoke(this, new(propertyName));
    }
}
