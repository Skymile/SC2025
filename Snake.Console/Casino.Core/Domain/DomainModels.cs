namespace Casino.Domain;

// Event
public abstract record DomainEvent;

public abstract record BetEvent(Player Player, Bet Bet) : DomainEvent;
public record BetPlaced(Player Player, Bet Bet) : BetEvent(Player, Bet);
public record BetLost(Player Player, Bet Bet) : BetEvent(Player, Bet);
public record BetWon(Player Player, Bet Bet) : BetEvent(Player, Bet);

public record RouletteSpun(Pocket Pocket) : DomainEvent;

// Command
public abstract record DomainCommand;
public record PlaceBet(Player Player, Bet Bet) : DomainCommand;

// Entities
public abstract record Bet(Guid Id, Amount Amount);
public record StraightBet(Guid Id, Amount Amount, Pocket Pocket)
    : Bet(Id, Amount);

public record Player(Guid Id);
