using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<TestClassDictionaryGet>();

public class TestClassListSize
{
    private const int Size = 1_000_000;

    [Benchmark]
    public void TestA()
    {
        var list = new List<int>();
        for (int i = 0; i < Size; i++)
            list.Add(i);
    }

    [Benchmark]
    public void TestB()
    {
        var list = new List<int>(Size);
        for (int i = 0; i < Size; i++)
            list.Add(i);
    }
}

public class TestClassDictionaryGet
{
    private const int Size = 100;

    private static readonly Dictionary<int, string> dict = Enumerable
        .Range(0, Size)
        .ToDictionary(i => i, i => i.ToString());

    private static string[] array = Enumerable
        .Range(0, Size)
        .Select(i => i.ToString())
        .ToArray();

    [Benchmark]
    public void TestDict()
    {
        int count = 0;
        for (int i = 0; i < Size; i++)
            if (dict.ContainsKey(i))
                ++count;
    }

    [Benchmark]
    public void TestArray()
    {
        int count = 0;
        for (int i = 0; i < Size; i++)
            if (array.Contains(i.ToString()))
                ++count;
    }
}