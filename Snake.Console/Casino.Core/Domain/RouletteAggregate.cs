namespace Casino.Domain;

// Aggregate Root
/// <summary>
/// A roulette containing pockets
/// </summary>
public sealed class RouletteAggregate : ReactiveBase
{
    private RouletteAggregate(Pocket[,] pockets)
    {
        rouletteService = new(this);
        Pockets = pockets;
    }

    public Pocket[,] Pockets { get; }

    public decimal GetPlayerWinnings(Player player) => (
        from ev in events.OfType<BetEvent>()
        where ev.Player.Id == player.Id
        select
            ev is BetWon ? ev.Bet.Amount.Value :
            ev is BetLost ? -ev.Bet.Amount.Value : 0m
    ).Sum();

    public override void ApplyEvent(DomainEvent domainEvent)
    {
        switch (domainEvent)
        {
            case BetPlaced bp:
                var result = rouletteService.GetRandomRoulette();
                SendEvent(new RouletteSpun(result));
                switch (bp.Bet)
                {
                    case StraightBet sb:
                        if (sb.Pocket.Value == result.Value)
                            SendEvent(new BetWon(bp.Player, bp.Bet));
                        else
                            SendEvent(new BetLost(bp.Player, bp.Bet));
                        break;
                }
                break;
        }
        events.Add(domainEvent);
    }

    public static Result<RouletteAggregate> TryCreate()
    {
        var r = new RouletteAggregate(new Pocket[RowLength, ColumnLength]);

        for (int row = 0; row < RowLength; row++)
            for (int col = 0; col < ColumnLength; col++)
                r.Pockets[row, col] = 
                    Pocket.TryCreate(row * ColumnLength + col + 1).Value;

        return Result.Create(r);
    }

    public const int RowLength = 12;
    public const int ColumnLength = 3;

    private readonly RouletteService rouletteService;
    private readonly List<DomainEvent> events = [];
}
