using Keystrokes.Core.Models;

namespace Keystrokes.Core.Services;

public interface IClassifier
{
    IEnumerable<int> Apply(Sample[] samples);
}
