using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

using Casino.Domain;
using Casino.Services.Base;
using Casino.ViewModels;

namespace Casino.Wpf;

using Brushes = System.Windows.Media.Brushes;

class OutputService : IOutputService
{
    public void WriteMessage(string message) =>
        Write(message);

    public void WriteBoard(RouletteAggregate roulette)
    {
        var uniformGrid = MainWindow.GlobalGridRoulette;
        for (int row = 0; row < 12; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                var pocket = roulette.Pockets[row, col];

                uniformGrid.Children.Add(new Label
                {
                    Content = pocket.Value.ToString(),
                    Foreground = Brushes.White,
                    Background = pocket.Color == Color.Black 
                        ? Brushes.Black : Brushes.Red,
                    Width = 64,
                    Height = 64,
                    FontSize = 32,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(1),
                });
            }
        }
    }

    public void WritePlayerCapital(Player player) =>
        Write($"You have {player.Capital.Value}$");

    private void Write(string str) =>
        MainWindow.GlobalTextBox.Text += str + Environment.NewLine;
}

class InputService(IOutputService outputSerivce) : IInputService
{
    public Amount GetAmount()
    {
        return new Amount(0);
    }

    public Pocket GetPocketNumber()
    {
        return new Pocket(0, Color.Black);
    }
}

public partial class MainWindow : Window
{
    public static TextBox GlobalTextBox;
    public static UniformGrid GlobalGridRoulette;

    private MainViewModel vm;
    private Player player;

    public MainWindow()
    {
        InitializeComponent();
        GlobalTextBox = this.MainTextBox;
        GlobalGridRoulette = this.GridRoulette;

        vm = new MainViewModel(
            new OutputService(),
            new InputService(new OutputService()),
            RouletteAggregate.TryCreate().Value
        );

        player = new Player(Guid.NewGuid())
        {
            Capital = Amount.TryCreate(10_000m).Value
        };
        vm.WriteBoard();
        vm.WritePlayerCapital(player);
        this.CapitalLeft.Content = $"Capital left: {player.Capital}";
        this.BetAmount.Content = this.BetSlider.Value = 1000;
    }

    private void Bet_Click(object sender, RoutedEventArgs e)
    {
        var amount = Amount.TryCreate((decimal)((int)this.BetSlider.Value / 100 * 100)).Value;

        if (player.Capital.Value < amount.Value)
            vm.CapitalTooLow();
        else
            vm.PlaceBet(
                player,
                new StraightBet(
                    Guid.NewGuid(),
                    amount,
                    vm.ChoosePocket())
            );

        if (player.Capital.Value < 100)
        {
            vm.Broke();
            this.CapitalLeft.Content = $"Capital left: {player.Capital}";
            this.CapitalLeft.Foreground = Brushes.Red; 
        }
        else
        {
            this.CapitalLeft.Content = $"Capital left: {player.Capital}";
        }
    }

    private void BetSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (this.BetAmount is not null)
            this.BetAmount.Content = (int)this.BetSlider.Value / 100 * 100;
    }
}