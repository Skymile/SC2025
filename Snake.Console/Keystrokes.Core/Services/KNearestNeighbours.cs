using Keystrokes.Core.Models;

namespace Keystrokes.Core.Services;

public class KNearestNeighbours(DistanceCallback distance, int k) : IClassifier
{
    public int Apply(Sample current, Sample[] samples) =>
        (
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

    public IEnumerable<int> Apply(Sample[] samples) =>
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
