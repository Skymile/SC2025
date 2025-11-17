using System.Windows;
using System.Windows.Input;

using Snake.Core;

namespace Snake.Desktop
{
    public class DesktopKeyMapper : IKeyMapper<Key>
    {
        public UserAction? ToUserAction(Key key) =>
            key switch
            {
                Key.D or Key.Right => UserAction.GoRight,
                Key.A or Key.Left  => UserAction.GoLeft,
                Key.W or Key.Up    => UserAction.GoUp,
                Key.S or Key.Down  => UserAction.GoDown,
                _ => null,
            };
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Key k = e.Key;

            ;
        }
    }
}