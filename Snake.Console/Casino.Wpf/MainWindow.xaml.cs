using System.Windows;

using Casino.Domain;

namespace Casino.Wpf;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.DataContext = vm = new MainViewModel(
            new OutputService(this.MainTextBox, this.GridRoulette),
            new InputService(new OutputService(this.MainTextBox, this.GridRoulette)),
            RouletteAggregate.TryCreate().Value,
            player = new Player(Guid.NewGuid())
            {
                Capital = Amount.TryCreate(10_000m).Value
            }
        );
        vm.Initialize();

        vm.WriteBoard();
        vm.WritePlayerCapital(player);
    }

    private readonly MainViewModel vm;
    private readonly Player player;
}