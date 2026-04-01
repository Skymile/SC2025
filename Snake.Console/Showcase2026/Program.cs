// https://github.com/Skymile/SC2025
// https://forms.gle/35mRahLQ1FTsMTq48
// https://discord.gg/9g4xP697c
// Visual Studio // JetBrains Rider

using System.Collections;

GameMenu selected = GameMenu.NewGame;
GameOptions options =
    GameOptions.IsMenuRevolving | 
    GameOptions.IsSelectedColored |
    GameOptions.IsSelectedInBrackets
    ;

while (true)
{
    Display(selected, options);
    
    (selected, options) = ReadInput(selected, options);

    Console.Clear();
    Console.WriteLine(options);
}

static void Display(GameMenu selected, GameOptions options)
{
    var defaultColor = Console.ForegroundColor;
    var names = Enum.GetNames<GameMenu>();
    for (int i = 0; i < names.Length; i++)
    {
        if (options.HasFlag(GameOptions.IsSelectedColored))
            Console.ForegroundColor = names[i] == selected.ToString()
                ? ConsoleColor.Cyan
                : defaultColor;

        Console.WriteLine(
            names[i] == selected.ToString() &&
            options.HasFlag(GameOptions.IsSelectedInBrackets)
                ? $"[{i + 1}]: {names[i]}"
                : $" {i + 1} : {names[i]}"
        );
    }
    Console.ForegroundColor = defaultColor;
}

static (GameMenu newSelection, GameOptions newOptions) 
    ReadInput(GameMenu previous, GameOptions options)
{
    var count = Enum.GetNames<GameMenu>().Length;
    var keyInfo = Console.ReadKey();

    return char.IsDigit(keyInfo.KeyChar)
        ? ((GameMenu)(keyInfo.KeyChar - '1'), options)
        : keyInfo.Key switch
        {
            ConsoleKey.DownArrow => 
                (options.HasFlag(GameOptions.IsMenuRevolving)
                    ? (GameMenu)(((int)previous + 1) % count)
                    : (GameMenu)Math.Min(count - 1, (int)previous + 1),
                 options),

            ConsoleKey.UpArrow =>
                (options.HasFlag(GameOptions.IsMenuRevolving)
                    ? (GameMenu)(((int)previous - 1 + count) % count)
                    : (GameMenu)Math.Max(0, (int)previous - 1),
                options),

            ConsoleKey.X => (
                previous, 
                Change(options, GameOptions.IsMenuRevolving, options.HasFlag(GameOptions.IsMenuRevolving))
            ),

            ConsoleKey.Z => (
                previous, 
                Change(options, GameOptions.IsSelectedColored, options.HasFlag(GameOptions.IsSelectedColored))
            ),

            ConsoleKey.C => (
                previous, 
                Change(options, GameOptions.IsSelectedInBrackets, options.HasFlag(GameOptions.IsSelectedInBrackets ))
            ),

            _ => (previous, options)
        };
}

static GameOptions Change(GameOptions option, GameOptions field, bool shouldRemove) =>
    shouldRemove 
        ? option & ~field
        : option | field;

//if (name == selected.ToString())
//    Console.ForegroundColor = ConsoleColor.Cyan;
//else
//    Console.ForegroundColor = defaultColor;

return;

int[] array = [1, 2, 3, 4]; // static (it cannot be extended)
int[] extend1 = array.Prepend(5).ToArray();
int[] extend2 = [.. array.Prepend(5)];

var queue = new Queue<int>();
queue.Enqueue(1);

var stack = new Stack<int>();
stack.Push(1);
Console.WriteLine(stack.Peek());
Console.WriteLine(stack.Pop());
Console.WriteLine(stack.TryPop(out int result) ? result : -1);

var list = new List<int>() { 1, 2, 3, 4 };
list.Add(5);

var custom = new CustomList<int>();
custom.Add(1);
custom.Add(2);
custom.Add(3);
custom.Add(4);

foreach (var i in custom)
    Console.WriteLine(i);

if (custom.TryFindIndex(3, out int index1))
    Console.WriteLine(index1);

if (custom.TryFindIndex(9, out int index2))
    Console.WriteLine(index2);

public enum GameMenu
{
    NewGame,
    LoadGame,
    Options,
    Credits,
    Exit
}

[Flags]
public enum GameOptions // int // 4 bytes -> 4 * 8 bits
{ 
    None                 = 0, // 0,
    IsMenuRevolving      = 1 << 0, // 1,
    IsSelectedColored    = 1 << 1, // 2, 
    IsSelectedInBrackets = 1 << 2, // 2, 
} // 0 1 2 4 8 16

class CustomList<T> : IEnumerable<T>
{
    public CustomList()
    {
    }

    public bool TryFindIndex(T value, out int index)
    {
        for (int i = 0; i < count; i++)
            if (array[i]?.Equals(value) == true)
            {
                index = i;
                return true;
            }

        index = -1;
        return false;
    }

    public void Add(T value)
    {
        if (count >= array.Length)
        {
            var tmp = new T[array.Length * 2]; // log n
            Array.Copy(array, tmp, array.Length);
            array = tmp;
        }

        array[count] = value;
        ++count;
    }

    public IEnumerator<T> GetEnumerator() =>
        array.Take(count).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => 
        array.Take(count).GetEnumerator();

    public int Count => count;
    public int Capacity => array.Length;

    private int count;
    private T[] array = new T[2];
}

/*
return;

(string Name, int Size)[] collection = [
    (typeof(int).Name   , sizeof(int)),
    (typeof(long).Name  , sizeof(long)),
    (typeof(char).Name  , sizeof(char)),
    (typeof(byte).Name  , sizeof(byte)), // 0; 255
    (typeof(sbyte).Name , sizeof(sbyte)), // -128; 127
    (typeof(long).Name  , sizeof(long)),
];

Console.WriteLine(string.Join(Environment.NewLine,
    collection.Select(i => string.Join(", ", i.Name, i.Size))));

Console.WriteLine();

foreach (var (Name, Size) in collection)
    Console.WriteLine(string.Join(", ", Name, Size));


return;

var fileHandler = new FileHandler("test.txt");
try
{
    // Do
    throw new Exception();
}
finally
{
    fileHandler.Dispose();
}

var display = new Output();
using (new Logger(display))
{
    display.Display("Abc");
    using (new Logger(display))
        display.Display("Abc");
    display.Display("Abc");

    Function(display);
}

void Function(Output display)
{
    using (new Logger(display))
    {
        display.Display("For loop: ");
        for (int i = 0; i < 4; i++)
        {
            using (new Logger(display))
                display.Display($"{i}");
        }
    }
}

class FileHandler : IDisposable
{
    private FileStream field;

    public FileHandler(string filename) =>
        field = File.Open(filename, FileMode.Open);

    public void Dispose() =>
        field.Close();
}

class Output
{
    public int Indent { get; set; }

    public void Display(string str) =>
        Console.WriteLine(new string(' ', Indent * 2) + str);
}

class Logger : IDisposable
{
    private readonly Output display;

    public Logger(Output display)
    {
        ++display.Indent;
        this.display = display;
    }

    public void Dispose() => --display.Indent;
}
*/