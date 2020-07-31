using QLearning.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace QLearning.Extensions
{
    public static class SquareListExtensions
    {
        public static Square GetSquareByState(this ObservableCollection<Square> squareList, int state)
            => squareList.FirstOrDefault(s => s.State.Id == state);
    }
}