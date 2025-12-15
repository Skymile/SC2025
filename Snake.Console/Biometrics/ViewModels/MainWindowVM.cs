using System.IO;
using System.Reflection;

using Biometrics.Core;
using Biometrics.ImageProcessing;

namespace Biometrics.ViewModels;

public class MainWindowVM
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
    }

    public IImage GetImage() => new Image(fileToPath[Files[SelectedFile]])
        .Apply(new Grayscale())
        .Apply(new MedianFilter())
        .Apply(new MedianFilter())
        .Apply(new ConvolutionFilter(windowDict[Windows[SelectedWindow]])
        {
            Sensitivity = 1.0
        })
        ;

    public int SelectedFile { get; set; }
    public string[] Files { get; set; }

    public int SelectedWindow { get; set; }
    public string[] Windows { get; set; } = windowDict.Keys.ToArray();

    private static readonly Dictionary<string, double[]> windowDict = typeof(Window3x3)
        .GetFields(BindingFlags.Public | BindingFlags.Static)
        .ToDictionary(i => i.Name, i => (double[])i.GetValue(null)!);

    private readonly Dictionary<string, string> fileToPath = [];
}
