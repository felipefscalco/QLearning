using QLearning.Abstractions;
using QLearning.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace QLearning.Helpers
{
    public class CanvasHelper : ICanvasHelper
    {
        private readonly int SquareSize = 65;
        private readonly int Columns = 10;
        private readonly int Rows = 5;
        private readonly IQTableHelper QTableHelper;

        public CanvasHelper(IQTableHelper qTableHelper)
        {
            QTableHelper = qTableHelper;
        }
        
        public ObservableCollection<Square> GetSquares()
        {
            var squares = new ObservableCollection<Square>();
            var random = new Random();
            
            var index = 0;

            for (int x = 0; x < Columns * SquareSize; x += SquareSize)
            {
                for (int y = 0; y < Rows * SquareSize; y += SquareSize)
                {
                    var currentState = QTableHelper.GetStateByMatrixCoordinate(x / SquareSize, y / SquareSize);
                    var newSquare = new Square
                    {
                        X = x,
                        Y = y,
                        Width = SquareSize,
                        Height = SquareSize,
                        Color = new SolidColorBrush(Color.FromRgb((byte)random.Next(0,100), 10, 10)),
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