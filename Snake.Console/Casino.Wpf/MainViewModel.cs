using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using Casino.Domain;
using Casino.Services.Base;

namespace Casino.Wpf;

using Brush = System.Windows.Media.Brush;
using Brushes = System.Windows.Media.Brushes;

public class MainViewModel(
        IOutputService outputService,
        IInputService inputService,
        RouletteAggregate roulette,
        Player player
    ) : ViewModels.MainViewModel(
        outputService,
        inputService,
        roulette
    ), INotifyPropertyChanged
{
    public ICommand? BetPlacedCommand { get; set => Set(ref field, value); }

    public bool    IsBetPlacingAllowed { get; set => Set(ref field, value); }
    public int     BetAmount           { get; set => Set(ref field, value / 100 * 100); }
    public string? CapitalLeft         { get; set => Set(ref field, value); }
    public Brush?  CapitalLeftColor    { get; set => Set(ref field, value); }

    public void Initialize()
    {
        this.BetPlacedCommand = new Command(BetClick);
        this.IsBetPlacingAllowed = true;
        this.CapitalLeftColor = Brushes.Black;
        this.CapitalLeft = $"Capital left: {player.Capital}";
    }

    public void BetClick()
    {
        var maybeAmount = Amount.TryCreate(BetAmount);

        if (maybeAmount.IsError)
            return;

        var amount = maybeAmount.Value;

        if (player.Capital.Value < amount.Value)
            CapitalTooLow();
        else
            PlaceBet(
                player,
                new StraightBet(
                    Guid.NewGuid(),
                    amount,
                    ChoosePocket())
            );

        if (player.Capital.Value < 100)
        {
            Broke();
            this.IsBetPlacingAllowed = false;
            this.CapitalLeft = $"Capital left: {player.Capital}";
            this.CapitalLeftColor = Brushes.Red;
        }
        else
        {
            this.CapitalLeft = $"Capital left: {player.Capital}";
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void Set<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
    {
        field = value;
        PropertyChanged?.Invoke(this, new(propertyName));
    }
}
