using QLearning.Abstractions;
using QLearning.Enums;
using System.Collections.Generic;

namespace QLearning.Helpers
{
    public class QTableHelper : IQTableHelper
    {
        private readonly int[,] MatrixStates = 
        {
            {5, 4, 3, 2, 1},
            {6, 7, 8, 9, 10},
            {15, 14, 13, 12, 11},
            {16, 17, 18, 19, 20},
            {25, 24, 23, 22, 21},
            {26, 27, 28, 29, 30},
            {35, 34, 33, 32, 31},
            {36, 37, 38, 39, 40},
            {45, 44, 43, 42, 41},
            {46, 47, 48, 49, 50}
        };

        public Dictionary<string, List<Direction>> States { get; }

        public QTableHelper()
        {
            States = InitializeStatesAndPossibleDirections();
        }

        public KeyValuePair<string, List<Direction>> GetStateByMatrixCoordinate(int row, int column)
        {
            var state = MatrixStates[row, column].ToString();
            var possibleDirections = States[state];
            return new KeyValuePair<string, List<Direction>>(state, possibleDirections);
        }

        private Dictionary<string, List<Direction>> InitializeStatesAndPossibleDirections()
        {
            var states = new Dictionary<string, List<Direction>>();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (CanGoToAllDirections(i, j))
                    {
                        var directions = new List<Direction>
                        {
                            Direction.North,
                            Direction.South,
                            Direction.West,
                            Direction.East
                        };

                        states.Add($"{MatrixStates[i, j]}", directions);
                    }
                    else if (CanGoSouthWestEast(i, j))
                    {
                        var directions = new List<Direction>
                        {
                            Direction.South,
                            Direction.West,
                            Direction.East
                        };

                        states.Add($"{MatrixStates[i, j]}", directions);
                    }
                    else if (CanGoNorthEastWest(i, j))
                    {
                        var directions = new List<Direction>
                        {
                            Direction.North,
                            Direction.East,
                            Direction.West
                        };

                        states.Add($"{MatrixStates[i, j]}", directions);
                    }
                    else if (CanGoNorthSouthEast(i, j))
                    {
                        var directions = new List<Direction>
                        {
                            Direction.North,
                            Direction.South,
                            Direction.East
                        };

                        states.Add($"{MatrixStates[i, j]}", directions);
                    }
                    else if (CanGoNorthSouthWest(i, j))
                    {
                        var directions = new List<Direction>
                        {
                            Direction.North,
                            Direction.South,
                            Direction.West
                        };

                        states.Add($"{MatrixStates[i, j]}", directions);
                    }
                    else
                    {
                        if (CanGoSouthEast(i, j))
                        {
                            var directions = new List<Direction>
                            {
                                Direction.South,
                                Direction.East
                            };

                            states.Add($"{MatrixStates[i, j]}", directions);
                        }
                        else if (CanGoNorthEast(i, j))
                        {
                            var directions = new List<Direction>
                            {
                                Direction.North,
                                Direction.East
                            };

                            states.Add($"{MatrixStates[i, j]}", directions);
                        }
                        else if (CanGoSouthWest(i, j))
                        {
                            var directions = new List<Direction>
                            {
                                Direction.South,
                                Direction.West,
                            };

                            states.Add($"{MatrixStates[i, j]}", directions);
                        }
                        else if (IsFinalState(i, j))
                        {
                            var directions = new List<Direction>
                            {
                                Direction.None
                            };

                            states.Add($"{MatrixStates[i, j]}", directions);
                        }
                    }
                }
            }

            return states;
        }

        private bool IsFinalState(int i, int j)
            => i == 9 && j == 4;

        private bool CanGoSouthWest(int i, int j)
            => i == 9 && j == 0;

        private bool CanGoNorthEast(int i, int j)
            => i == 0 && j == 4;

        private bool CanGoSouthEast(int i, int j)
            => i == 0 && j == 0;

        private bool CanGoNorthSouthWest(int i, int j)
            => i == 9 && j > 0 && j < 4;

        private bool CanGoNorthSouthEast(int i, int j)
            => i == 0 && j > 0 && j < 4;

        private bool CanGoNorthEastWest(int i, int j)
            => j == 4 && i > 0 && i < 9;

        private bool CanGoSouthWestEast(int i, int j)
            => j == 0 && i > 0 && i < 9;

        private bool CanGoToAllDirections(int i, int j)
            => j > 0 && i > 0 && j < 4 && i < 9;
    }
}