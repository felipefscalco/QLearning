using QLearning.Abstractions;
using QLearning.Enums;
using QLearning.Extensions;
using QLearning.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLearning.Helpers
{
    public class StateHelper : IStateHelper
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

        private readonly List<State> States;

        public StateHelper()
        {
            States = InitializeStatesAndPossibleDirections();
        }

        public State GetNextStateByDirection(int row, int column, Direction direction)
        {
            int state;

            switch (direction)
            {
                case Direction.North:
                    state = MatrixStates[row, column - 1];
                    return States.GetStateById(state);

                case Direction.South:
                    state = MatrixStates[row, column + 1];
                    return States.GetStateById(state);

                case Direction.West:
                    state = MatrixStates[row - 1, column];
                    return States.GetStateById(state);

                case Direction.East:
                    state = MatrixStates[row + 1, column];
                    return States.GetStateById(state);

                default:
                    throw new ArgumentException("Not a possible move.");
            }
        }

        public State GetStateByMatrixCoordinates(int row, int column)
        {
            var state = MatrixStates[row, column];

            return States.FirstOrDefault(s => s.Id.Equals(state));
        }

        public List<State> GetStatesAndPossibleDirections()
            => States;

        private List<State> InitializeStatesAndPossibleDirections()
        {
            var states = new List<State>();

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

                        var stateId = MatrixStates[i, j];
                        states.Add(new State(stateId, directions));
                    }
                    else if (CanGoSouthWestEast(i, j))
                    {
                        var directions = new List<Direction>
                        {
                            Direction.South,
                            Direction.West,
                            Direction.East
                        };

                        var stateId = MatrixStates[i, j];
                        states.Add(new State(stateId, directions));
                    }
                    else if (CanGoNorthEastWest(i, j))
                    {
                        var directions = new List<Direction>
                        {
                            Direction.North,
                            Direction.East,
                            Direction.West
                        };

                        var stateId = MatrixStates[i, j];
                        states.Add(new State(stateId, directions));
                    }
                    else if (CanGoNorthSouthEast(i, j))
                    {
                        var directions = new List<Direction>
                        {
                            Direction.North,
                            Direction.South,
                            Direction.East
                        };

                        var stateId = MatrixStates[i, j];
                        states.Add(new State(stateId, directions));
                    }
                    else if (CanGoNorthSouthWest(i, j))
                    {
                        var directions = new List<Direction>
                        {
                            Direction.North,
                            Direction.South,
                            Direction.West
                        };

                        var stateId = MatrixStates[i, j];
                        states.Add(new State(stateId, directions));
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

                            var stateId = MatrixStates[i, j];
                            states.Add(new State(stateId, directions));
                        }
                        else if (CanGoNorthEast(i, j))
                        {
                            var directions = new List<Direction>
                            {
                                Direction.North,
                                Direction.East
                            };

                            var stateId = MatrixStates[i, j];
                            states.Add(new State(stateId, directions));
                        }
                        else if (CanGoSouthWest(i, j))
                        {
                            var directions = new List<Direction>
                            {
                                Direction.South,
                                Direction.West,
                            };

                            var stateId = MatrixStates[i, j];
                            states.Add(new State(stateId, directions));
                        }
                        else if (IsFinalState(i, j))
                        {
                            var directions = new List<Direction>
                            {
                                Direction.None
                            };

                            var stateId = MatrixStates[i, j];
                            states.Add(new State(stateId, directions));
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