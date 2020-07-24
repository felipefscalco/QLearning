using QLearning.Models;
using System.Collections.ObjectModel;

namespace QLearning.Abstractions
{
    public interface ICanvasHelper
    {
        ObservableCollection<Square> GetSquares();
    }
}