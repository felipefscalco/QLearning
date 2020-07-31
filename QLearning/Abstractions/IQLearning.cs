using QLearning.Models;
using System.Collections.ObjectModel;

namespace QLearning.Abstractions
{
    public interface IQLearning
    {
        void StartQLearning(ObservableCollection<Square> squares);
    }
}