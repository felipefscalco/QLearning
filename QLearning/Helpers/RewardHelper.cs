using QLearning.Abstractions;
using System.Windows.Media;

namespace QLearning.Helpers
{
    public class RewardHelper : IRewardHelper
    {
        private const string HexBlueColor = "#FF32B4FF";
        private const string HexGreenColor = "#FF00DC2D";
        private const string HexBlackColor = "#FF0A0A0A";

        public double GetReward(SolidColorBrush color, int state)
        {
            if (IsGreenColor(color))
            {
                if (state == 1) // initial state
                    return -10;

                return 100;
            }

            if (IsBlackColor(color))
                return -100;

            if (IsBlueColor(color))
                return -1;

            return 0;
        }

        private bool IsBlueColor(SolidColorBrush color)
            => color.ToString().Equals(HexBlueColor);

        private bool IsBlackColor(SolidColorBrush color)
            => color.ToString().Equals(HexBlackColor);

        private bool IsGreenColor(SolidColorBrush color)
            => color.ToString().Equals(HexGreenColor);
    }
}