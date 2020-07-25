using System.Windows.Media;

namespace QLearning.Abstractions
{
    public interface IRewardHelper
    {
        double GetReward(SolidColorBrush color, int sate);
    }
}