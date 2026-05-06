using Casino.Domain;
using Casino.Services;
using Casino.ViewModels;

var vm = new MainViewModel(
    new OutputService(),
    new InputService(new OutputService()),
    RouletteAggregate.TryCreate().Value
);

var player = new Player(Guid.NewGuid())
{
    Capital = Amount.TryCreate(10_000m).Value
};

while (player.Capital.Value > 100)
{
    vm.WriteBoard();
    vm.WritePlayerCapital(player);
    var amount = vm.ChooseAmount();
    
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
}
vm.Broke();