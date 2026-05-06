using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

using Casino.Domain;
using Casino.Services.Base;

namespace Casino.Wpf;

using Brushes = System.Windows.Media.Brushes;

class OutputService(
        TextBox textBox,
        UniformGrid uniformGrid
    ) : IOutputService
{
    public void WriteMessage(string message) =>
        Write(message);

    public void WriteBoard(RouletteAggregate roulette)
    {
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
        textBox.Text += str + Environment.NewLine;
}
