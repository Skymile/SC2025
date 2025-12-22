namespace Biometrics.Services
{
    public interface IAlgorithmService
    {
        string[] GetAlgorithmNames();
        Dictionary<string, double[]> GetAlgorithmWindows();
        string[] GetFilenames(string directory);
        Dictionary<string, string> GetFileToPathMap(string directory);
    }
}