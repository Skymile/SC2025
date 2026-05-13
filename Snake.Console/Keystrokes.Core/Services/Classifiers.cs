using Keystrokes.Core.Models;

namespace Keystrokes.Core.Services;

public record Classifiers
{
    // K-nearest neighbours
    public static IEnumerable<int> KNN(Sample[] samples, DistanceCallback distance, int k) =>
        from current in samples
        select (
            from sample in samples
            where current.SampleId != sample.SampleId
            let dist = distance(current.DwellTimes, sample.DwellTimes)
            orderby dist
            select sample
        ).Take(k)
         .GroupBy(i => i.UserId)
         .OrderByDescending(i => i.Key)
         .First()
         .Key;
}
