using System.Collections.Frozen;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Channels;
using System.Xml.Linq;

{
    if (true)
    {
    }
    else
    {
    }

    int value = 4;

    value = value switch
    {
        1 => 10,
        _ => 4
    };

    switch (value)
    {
        case 2:
        case 1:
            break;
        case 3:
            goto case 2;
        default:
            break;
    }

    while (true)
    {
    }

    do
    {
    } while (true);

    for (int i = 0; i < 10; i++)
    {
        continue;
        break;
    }

    for (int i = 0; i < 2; i++)
        for (int j = 0; j < 4; i++)
            goto Label2;
        Label2: // goto considered harmful

    static void Sample()
    {
        for (int i = 0; i < 2; i++)
            for (int j = 0; j < 4; i++)
                return;
    }

    for (int i = 10 - 1; i >= 0; i--)
    {
    }

    string ternaryOperator = true ? "yes" : "no";

    int a = 4_000_000; // sizeof = 4
    int k = sizeof(double);
    long l = 4_000_000_000_000_000_000L; // sizeof = 8
    long longMax = long.MaxValue;
    long longMin = long.MinValue;
    float pi = 3.14f;
    double e = 2.71828;
    decimal d = 1.61m;
    byte b = 42; // unsigned char 0-255
    byte minByte = byte.MinValue;
    byte bb = 0b_0010_0111;
    var big = new BigInteger(new byte[] { 255, 255 });
    int k1 = true ? 0 : 255;
    byte k2 = true ? byte.MinValue : byte.MaxValue;
    short s = -32000; // sizeof = 2
    ushort us = 65000; // sizeof = 2
    bool boolean = true;
    bool? triState = null;
    char c = 'A'; // character // sizeof = 2
    char c2 = 'Ł';
    sbyte sb = 43; // -128 127
    uint bbsbs = 4;

    int z1 = int.Parse("4");
    bool tryParse = int.TryParse("4", out int result);
    Console.WriteLine(result);
    int convert = Convert.ToInt32(4.0);
    var g = 4;

    var dict = new Dictionary<string, int>();

    string str = "str";
}

#region MyRegion

#endregion

{
    int[] arr = new int[3];
    // byte[]  sizeof(byte) 1
    // int[]   sizeof(int) 4
    // sizeof(int) * 3 = 12
    // arr[1]  =>  ((*arr) + sizeof(int)*1) & sizeof(int)

    int[][] jaggedArray = new int[4][];
    int[,] multidimArray = new int[4, 2];

    int[] k = [..
        from i in jaggedArray
        from j in i
        where j % 2 == 0
        select j
    ];

    for (int i = 0; i < multidimArray.GetLength(0); i++)
    {

    }

    var list = new List<int>(16) { 1, 2, 3, 4 };
    // Stały dostęp gdy mamy indeks O(1)
    // Dodawanie stała złożoność obliczeniowa O(1)
    // oprócz gdy trzeba zrobić O(n)
    list.Add(1);
    // int[8]
    //list[5] = 1;
    list.Add(1);
    list.Add(1);
    list.Add(1);
    list.Add(1);
    // int[16]
    // list[8] = 1;

    var queue = new Queue<int>();
    queue.Enqueue(4);
    // .Enqueue(4)
    // .Enqueue(2)
    // .Dequeue() = 4
    // .Dequeue() = 2
    // Usuwanie i dodawanie, dostęp do najstarszego

    var stack = new Stack<int>();
    stack.Push(1);
    stack.Push(2);
    stack.Push(3);
    stack.Push(4);
    stack.Pop();
    stack.Peek();
    // .Push(4)
    // .Push(2)
    // .Pop() = 2
    // .Pop() = 4
    // Dodawanie, usuwanie, dostęp do najnowszego

    var hashset = new HashSet<int>();
    hashset.Add(4);
    hashset.Contains(4); // O(1)

    var dict1 = new Dictionary<int, string>()
    {
        { 4, "asd" }
    };
    var dict2 = new Dictionary<int, string>()
    {
        [4] = "asd"
    };

    dict1[4] = "asd";

    var sortedSet = new SortedSet<string>();
    var sortedList = new SortedList<string, int>();
    var sortedDictionary = new SortedDictionary<string, int>();

    var immutableList = ImmutableList.Create(4, 2, 1);
    var frozenSet = FrozenSet.Create(4, 2, 1);

    var tuple1 = (First: 4, Second: 2);
    var tuple2 = ValueTuple.Create(1, 2);
    var tuple3 = Tuple.Create(1, 2);

    (int a, int b) = tuple1;
    var c = new MyClass();
    (int d, int f) = c;
}

var point2 = new Point2(2, 4);
var point3 = new Point3(2, 4);

enum Direction : short
{
    Up, Down = 40, Left, Right
}

[Flags]
enum StringSplitOptions : byte
{
    None        = 0b_0000_0001,
    Trim        = 0b_0000_0010,
    RemoveEmpty = 0b_0000_0100,
}

class StandardClass
{
    public StandardClass()
    {
        var options = 
            StringSplitOptions.Trim | 
            StringSplitOptions.RemoveEmpty;

        if ((int)(options & StringSplitOptions.RemoveEmpty) > 0)
            ;

        if (options.HasFlag(StringSplitOptions.Trim))
            ;
    }
}

class DerivedClass : MyClass2, IInterface1, IInterface2
{
    public void Abc() => throw new NotImplementedException();
    public void Abc2() => throw new NotImplementedException();

    public sealed override void AbstractMethod() => 
        throw new NotImplementedException();

    private class InnerClass : IInterface2
    {
        public int Property { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Abc2() => throw new NotImplementedException();
    }
}

partial class PartialClass
{
    public void Test() =>
        Debug();

    partial void Debug();

#if DEBUG
    partial void Debug() =>
        Console.WriteLine();
#elif TRACE
    partial void Debug() =>
        Console.WriteLine("sbs");
#endif
}

interface IInterface1
{
    void Abc();

    public void Test() =>
        Console.WriteLine("asd");
}

interface IInterface2
{
    void Abc2();
    int Property { get; set; }

    public void Test2() =>
        Console.WriteLine("asd");
}

abstract class MyClass2
{
    protected int field = 0;

    public abstract void AbstractMethod();
    public virtual void VirtualMethod() =>
        Console.WriteLine("Abc");

    public MyClass2()
    {
        Property = 4;
    }

    public int Property { get; set => field = value; }
}

class MyClass
{
    public override int GetHashCode() =>
        HashCode.Combine(Seconds);

    public void Deconstruct(out int a, out int b)
    {
        a = Seconds;
        b = Minutes;
    }

    public int Seconds { get; set; }
    public int Minutes { get; set; }

    //public string this[int index]
    //{
    //    get { /* return the specified index here */ }
    //    set { /* set the specified index to value here */ }
    //}

    static int A = B + 1;
    static int B = A + 4;

    #region Ctors
    public MyClass()
    {
        #region MyRegion
        #endregion // Code smell
    }

    public MyClass(int a)
    {
    }
    #endregion
}

// class        // Reference type
// struct       // Value type
struct Point(int x, int y)
{
    public void Test()
    {
        var p1 = new Point(2, 4) { MyProperty = 1 };
        var p2 = new Point(2, 4) { MyProperty = 1 };

        Console.WriteLine(p1.Equals(p2));
        Console.WriteLine(p1 == p2);
    }

    public readonly int X = x;
    public readonly int Y = y;

    public static bool PatternMatching(Point l) =>
        l is { X: 4, Y: 2 };

    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj is Point p && p == this;

    public static bool operator ==(Point l, Point r) =>
        l.X == r.X && l.Y == r.Y;
    public static bool operator !=(Point l, Point r) =>
        !(l == r);

    public required int MyProperty { get; init; }
}

record struct Point2(int X, int Y);
readonly record struct Point3(int X, int Y);
record PointClass(int X, int Y)
{
}

class TestClass
{
    public void Test1()
    {
        string str = "";
        for (int i = 0; i < 20_000; i++)
            str += "AbcAbcAbcAbcAbc";

        var sb = new StringBuilder();
        for (int i = 0; i < 20_000; i++)
            sb.Append("AbcAbcAbcAbcAbc");
        string str2 = sb.ToString();
    }

    public void Test2(IEnumerable<int> sequence)
    {
        for (int i = 0; i < sequence.Count(); i++)
            Console.WriteLine(sequence.ElementAt(i));

        foreach (var i in sequence)
            // Lazy loading
            Console.WriteLine(i);

        var list = sequence.ToList(); // Eager loading
        for (int i = 0; i < list.Count; i++)
            Console.WriteLine(list[i]);
    }
}

public record User(string Name, string Password)
{
    public void Test()
    {
        byte[] arr = new byte[4];
        unsafe
        {
            fixed (byte* ptr = arr)
            {
                int* stack = stackalloc int[4];
            }
        }

        var str = new StringBuilder()
            .AppendLine("4342")
            .ToString();

        User user1 = new UserBuilder()
            .SetPassword("password")
            .SetName("Username")
            .Build();

        User user2 = new UserBuilder2()
            .SetPassword("password")
            .SetName("Username")
            .User;

        // TextField()
        //  .Background(Color.gray)
        //  .Frame(width: 400, height: 500)
    }
}

public class UserBuilder
{
    public UserBuilder SetName(string name)
    {
        this.name = name;
        return this;
    }

    public UserBuilder SetPassword(string password)
    {
        this.password = password;
        return this;
    }

    public User Build() => new(name, password);

    private string name;
    private string password;
}

public class UserBuilder2
{
    public UserBuilder2 SetName(string name)
    {
        User = User with { Name = name };
        return this;
    }

    public UserBuilder2 SetPassword(string password)
    {
        User = User with { Password = password };
        return this;
    }

    public User User { get; private set; } = new("Anonymous", string.Empty);
}


