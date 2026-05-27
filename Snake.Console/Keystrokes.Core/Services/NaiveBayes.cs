using Keystrokes.Core.Models;

namespace Keystrokes.Core.Services;

public class ClassifierStrategy
{
    public required IClassifier Classifier { get; set; }

    public IEnumerable<int> Apply(Sample[] samples) =>
        Classifier?.Apply(samples) ?? [];
}

public class NaiveBayes(DistanceCallback distance, int k) : IClassifier
{
    public IEnumerable<int> Apply(Sample[] samples)
    {
        int[] labels = samples.Select(i => i.UserId).Distinct().ToArray();
        var labelsToCount = labels.ToDictionary(i => i, i => 0);
        foreach (var i in samples)
            ++labelsToCount[i.UserId];
        foreach (var current in samples)
        {
            var closeSamples = (
               from sample in samples
               where sample.SampleId != current.SampleId
               orderby distance(current.DwellTimes, sample.DwellTimes)
               select sample.UserId
            ).Take(k);
            var closeLabelsToCount = labels.ToDictionary(i => i, i => 0);
            foreach (var c in closeSamples)
                ++closeLabelsToCount[c];

            double[] chances = [..
                from label in labels
                let all = labelsToCount[label]
                let close = closeLabelsToCount[label]
                select (double)close / all
            ];

            yield return chances
                .Select((Chance, index) => (Chance, index))
                .OrderByDescending(i => i.Chance)
                .FirstOrDefault().index
                ;
        }
    }
}
