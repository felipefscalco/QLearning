using QLearning.Enums;
using QLearning.Models;
using System.Collections.Generic;

namespace QLearning.Abstractions
{
    public interface IStateHelper
    {
        State GetStateByMatrixCoordinates(int row, int column);

        State GetNextStateByDirection(int row, int column, Direction direction);

        List<State> GetStatesAndPossibleDirections();
    }
}