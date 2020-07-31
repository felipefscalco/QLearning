using QLearning.Models;
using System.Collections.ObjectModel;

namespace QLearning.Abstractions
{
    public interface IQLearningHandler
    {
        void PerformStep();

        void InitializeQLearning(ObservableCollection<Square> squares);
    }
}