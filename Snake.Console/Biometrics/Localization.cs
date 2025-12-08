namespace Biometrics
{
    public class Localization
    {
        private Localization() { }

        public static Localization Instance { get; } = new();

        public string Title => "Biometrics Application";
    }
}