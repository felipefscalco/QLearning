using QLearning.Enums;
using System.Collections.Generic;

namespace QLearning.Models
{
    public class State
    {
        public int Id { get; }

        public List<Direction> PossibleDirections { get; }

        public State(int id, List<Direction> possibleDirections)
        {
            Id = id;
            PossibleDirections = possibleDirections;
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}