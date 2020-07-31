using QLearning.Abstractions;
using System.Collections.Generic;
using System.Windows.Media;

namespace QLearning.Helpers
{
    public class ColorHelper : IColorHelper
    {
        private readonly Color BlueColor = Color.FromRgb(50, 180, 255);
        private readonly Color BlackColor = Color.FromRgb(10, 10, 10);
        private readonly Color GreenColor = Color.FromRgb(0, 220, 45);
        private readonly Color RedColor = Color.FromRgb(150, 0, 0);
        private readonly List<int> BlackSquares;

        public ColorHelper()
        {
            BlackSquares = new List<int>
            {
                7, 10, 11, 14 , 19, 20, 21, 24, 27, 30, 31, 37, 39, 40, 41
            };
        }

        public SolidColorBrush GetColor(int state)
        {
            if (state == 1 || state == 50) // initial and final state
                return new SolidColorBrush(GreenColor);

            if (IsBlackSquare(state))
                return new SolidColorBrush(BlackColor);

            return new SolidColorBrush(BlueColor);
        }

        public SolidColorBrush GetRedColor()
            => new SolidColorBrush(RedColor);

        private bool IsBlackSquare(int state)
            => BlackSquares.Contains(state);
    }
}