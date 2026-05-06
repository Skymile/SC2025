using Casino.Domain;
using Casino.Services.Base;

namespace Casino.ViewModels;

public class MainViewModel(
        IOutputService outputService,
        IInputService inputService,
        RouletteAggregate roulette
    ) : ReactiveBase
{
    public void CapitalTooLow() =>
        outputService.WriteMessage("You have too low capital to place the bet!");

    public void Broke() =>
        outputService.WriteMessage("You have no money left!");

    public void PlaceBet(Player player, Bet bet) =>
        SendEvent(new BetPlaced(player, bet));

    public Amount ChooseAmount() =>
        inputService.GetAmount();

    public Pocket ChoosePocket() =>
        inputService.GetPocketNumber();

    public void WriteBoard() =>
        outputService.WriteBoard(roulette);

    public void WritePlayerCapital(Player player) =>
        outputService.WritePlayerCapital(player);

    public override void ApplyEvent(DomainEvent domainEvent)
    {
        switch (domainEvent)
        {
            case RouletteSpun rs:
                outputService.WriteMessage($"Roulette spun: {rs.Pocket.Value} {rs.Pocket.Color}");
                break;

            case BetLost bl:
                outputService.WriteMessage($"Player {bl.Player.Id.GetHashCode()} lost {bl.Bet.Amount}");
                break;

            case BetWon bw:
                outputService.WriteMessage($"Player {bw.Player.Id.GetHashCode()} won {bw.Bet.Amount}");
                break;
        }
    }
}
