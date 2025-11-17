using System.Windows.Input;

using Snake.Core.Enums;
using Snake.Core.Interfaces;

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
}