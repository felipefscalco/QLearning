using QLearning.Abstractions;
using QLearning.Models;
using System.Collections.ObjectModel;

namespace QLearning.Helpers
{
    public class CanvasHelper : ICanvasHelper
    {
        private readonly IStateHelper StateHelper;
        private readonly IRewardHelper RewardHelper;
        private readonly IColorHelper ColorHelper;
        private readonly int SquareSize = 65;
        private readonly int Columns = 10;
        private readonly int Rows = 5;

        public CanvasHelper(IStateHelper stateHelper, IRewardHelper rewardHelper, IColorHelper colorHelper)
        {
            StateHelper = stateHelper;
            RewardHelper = rewardHelper;
            ColorHelper = colorHelper;
        }

        public ObservableCollection<Square> GetSquares()
        {
            var squares = new ObservableCollection<Square>();

            for (int x = 0; x < Columns * SquareSize; x += SquareSize)
            {
                for (int y = 0; y < Rows * SquareSize; y += SquareSize)
                {
                    var currentState = StateHelper.GetStateByMatrixCoordinates(x / SquareSize, y / SquareSize);
                    var color = ColorHelper.GetColor(currentState.Id);
                    var reward = RewardHelper.GetReward(color, currentState.Id);

                    var newSquare = new Square(x, y, SquareSize, SquareSize, color, reward, currentState);

                    squares.Add(newSquare);
                }
            }

            return squares;
        }
    }
}