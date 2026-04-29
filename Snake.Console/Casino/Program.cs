using Casino.Domain;
using Casino.Services;
using Casino.ViewModels;

var vm = new MainViewModel(
    new OutputService(),
    new InputService(new OutputService()),
    RouletteAggregate.TryCreate().Value
);

var player = new Player(Guid.NewGuid());

//vm.WriteBoard();

vm.PlaceBet(
    player,
    new StraightBet(
        Guid.NewGuid(), 
        vm.ChooseAmount(), 
        vm.ChoosePocket())
);
