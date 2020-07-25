using QLearning.Abstractions;
using QLearning.Models;
using System.Collections.ObjectModel;

namespace QLearning.Helpers
{
    public class CanvasHelper : ICanvasHelper
    {
        private readonly IQTableHelper QTableHelper;
        private readonly IRewardHelper RewardHelper;
        private readonly IColorHelper ColorHelper;
        private readonly int SquareSize = 65;
        private readonly int Columns = 10;
        private readonly int Rows = 5;

        public CanvasHelper(IQTableHelper qTableHelper, IRewardHelper rewardHelper, IColorHelper colorHelper)
        {
            QTableHelper = qTableHelper;
            RewardHelper = rewardHelper;
            ColorHelper = colorHelper;
        }
        
        public ObservableCollection<Square> GetSquares()
        {
            var squares = new ObservableCollection<Square>();
            
            var index = 0;

            for (int x = 0; x < Columns * SquareSize; x += SquareSize)
            {
                for (int y = 0; y < Rows * SquareSize; y += SquareSize)
                {
                    var currentState = QTableHelper.GetStateByMatrixCoordinate(x / SquareSize, y / SquareSize);
                    int.TryParse(currentState.Key, out var state);
                    var color = ColorHelper.GetColor(state);

                    var newSquare = new Square
                    {
                        X = x,
                        Y = y,
                        Width = SquareSize,
                        Height = SquareSize,
                        Color = color,
                        Reward = RewardHelper.GetReward(color, state),
                        State = currentState.Key,
                        PossibleDirections = currentState.Value,
                        IndexAtList = index,
                    };

                    squares.Add(newSquare);

                    index++;
                }
            }

            return squares;
        }
    }
}