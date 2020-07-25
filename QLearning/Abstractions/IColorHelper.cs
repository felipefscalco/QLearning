using System.Windows.Media;

namespace QLearning.Abstractions
{
    public interface IColorHelper
    {
        SolidColorBrush GetColor(int state);
    }
}