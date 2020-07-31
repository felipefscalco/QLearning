using QLearning.Models;
using System.Collections.Generic;
using System.Linq;

namespace QLearning.Extensions
{
    public static class StateListExtensions
    {
        public static State GetStateById(this List<State> stateList, int id)
            => stateList.FirstOrDefault(s => s.Id == id);
    }
}