using System.IO;
using System.Reflection;

using Biometrics.Core;

namespace Biometrics.Services;

public class AlgorithmService : IAlgorithmService
{
    public string[] GetFilenames(string directory) =>
        [.. Directory.EnumerateFiles("../../../Samples/")
                .Select(Path.GetFileNameWithoutExtension)
                .OfType<string>()];

    public Dictionary<string, string> GetFileToPathMap(string directory) =>
        Directory.EnumerateFiles("../../../Samples/")
            .ToDictionary(
                i => Path.GetFileNameWithoutExtension(i)
                    ?? throw new NullReferenceException(),
                i => i
            );

    public Type GetAlgorithmType(string name) =>
        (algorithmDict ??= typeof(Algorithm).Assembly
            .GetTypes()
            .Where(type => type.IsSubclassOf(typeof(Algorithm)) && !type.IsAbstract)
            .ToDictionary(type => type.Name, type => type)
        )[name];

    public string[] GetAlgorithmNames() =>
        [.. from type in typeof(Algorithm).Assembly.GetTypes()
            where type.IsSubclassOf(typeof(Algorithm)) && !type.IsAbstract
            select type.Name];

    public Dictionary<string, double[]> GetAlgorithmWindows() =>
        windowDict ??= typeof(Window3x3)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .ToDictionary(i => i.Name, i => (double[])i.GetValue(null)!);

    private static Dictionary<string, double[]>? windowDict;
    private static Dictionary<string, Type>? algorithmDict;
}
