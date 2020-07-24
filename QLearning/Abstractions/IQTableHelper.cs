using QLearning.Enums;
using System.Collections.Generic;

namespace QLearning.Abstractions
{
    public interface IQTableHelper
    {
        KeyValuePair<string, List<Direction>> GetStateByMatrixCoordinate(int row, int column);
    }
}