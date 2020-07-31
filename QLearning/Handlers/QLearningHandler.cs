using MoreLinq;
using Prism.Events;
using QLearning.Abstractions;
using QLearning.Enums;
using QLearning.Extensions;
using QLearning.Messages;
using QLearning.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace QLearning.Handlers
{
    public class QLearningHandler : IQLearningHandler
    {
        private readonly IEventAggregator EventAggregator;
        private readonly IStateHelper StateHelper;
        private readonly IColorHelper ColorHelper;
        private readonly Random Randomizer;
        private const int InitialState = 1;
        private const int FinalState = 50;
        private const double Gamma = 0.5;

        private ObservableCollection<Square> _squares;
        private ObservableCollection<QLine> _qTable;
        private ObservableCollection<QLine> _qTableAux;
        private Square _currentSquare;
        private Square _previousSquare;
        private int _currentState;
        private bool _bestPathFound;

        public QLearningHandler(IEventAggregator eventAggregator, IStateHelper stateHelper, IColorHelper colorHelper)
        {
            EventAggregator = eventAggregator;
            StateHelper = stateHelper;
            ColorHelper = colorHelper;
            Randomizer = new Random();
        }

        public void InitializeQLearning(ObservableCollection<Square> squares)
        {
            _currentState = InitialState;
            _squares = squares;
            _qTable = InitializeQTable();
            _qTableAux = InitializeQTable();

            EventAggregator.GetEvent<SetQTableMessage>().Publish(_qTable);
            EventAggregator.GetEvent<SetInitialStateMessage>().Subscribe(() => _currentState = InitialState);
        }

        public void PerformStep()
        {
            if (IsFinalState())
            {
                _bestPathFound = VerifyIfQTableHasConverged();

                UpdateCurrentSquare(ColorHelper.GetColor(FinalState));

                SetCurrentState(InitialState);

                EventAggregator.GetEvent<UpdateEpisodesMessage>().Publish();

                if (_bestPathFound)
                    return;
            }

            SetCurrentSquare();
            SetPreviousSquare();

            var possibleNextSquaresToMove = GetPossibleSquaresToMove();

            var selectedSquareToMove = GetNextSquareToMove(possibleNextSquaresToMove);

            UpdateQTable(selectedSquareToMove);

            SetCurrentSquare(selectedSquareToMove.Value);
            SetCurrentState(_currentSquare.State.Id);

            UpdateCurrentSquare();
            UpdatePreviousSquare();
        }

        private void SetCurrentSquare()
            => _currentSquare = _squares.FirstOrDefault(s => s.State.Id == _currentState);

        private void SetCurrentSquare(Square square)
            => _currentSquare = square;

        private void SetPreviousSquare()
            => _previousSquare = _currentSquare;

        private void SetCurrentState(int state)
            => _currentState = state;

        private bool IsFinalState()
            => _currentState.Equals(FinalState);

        private void UpdateCurrentSquare()
            => _currentSquare.SetColor(ColorHelper.GetRedColor());

        private void UpdateCurrentSquare(SolidColorBrush color)
            => _currentSquare.SetColor(color);

        private void UpdatePreviousSquare()
        {
            var color = ColorHelper.GetColor(_previousSquare.State.Id);
            _previousSquare.SetColor(color);
        }

        private List<KeyValuePair<Direction, Square>> GetPossibleSquaresToMove()
        {
            var possibleSquares = new List<KeyValuePair<Direction, Square>>();
            var row = _currentSquare.GetRow();
            var column = _currentSquare.GetColumn();

            foreach (var direction in _currentSquare.State.PossibleDirections)
            {
                var possibleNextState = StateHelper.GetNextStateByDirection(row, column, direction);
                var square = _squares.GetSquareByState(possibleNextState.Id);
                possibleSquares.Add(new KeyValuePair<Direction, Square>(direction, square));
            }

            return possibleSquares;
        }

        private KeyValuePair<Direction, Square> GetNextSquareToMove(List<KeyValuePair<Direction, Square>> possibleNextSquaresToMove)
        {
            KeyValuePair<Direction, Square> selectedSquareToMove;
            var probability = Randomizer.NextDouble();

            if (probability > .3 || _bestPathFound)
                selectedSquareToMove = GetSquareWithBestReward(possibleNextSquaresToMove);
            else // random way
            {
                var randomIndex = Randomizer.Next(0, possibleNextSquaresToMove.Count);
                selectedSquareToMove = possibleNextSquaresToMove.ElementAt(randomIndex);
            }

            return selectedSquareToMove;
        }

        private KeyValuePair<Direction, Square> GetSquareWithBestReward(List<KeyValuePair<Direction, Square>> directionAndNextSquares)
        {
            List<QLine> qLines = GetQLinesFromNextSquares(directionAndNextSquares);

            var qLineWithBestReward = GetQLineWithBestReward(qLines);

            var nextState = StateHelper.GetNextStateByDirection(_currentSquare.GetRow(), _currentSquare.GetColumn(), qLineWithBestReward.Direction);
            var squareWithBestReward = _squares.GetSquareByState(nextState.Id);

            return new KeyValuePair<Direction, Square>(qLineWithBestReward.Direction, squareWithBestReward);
        }

        private List<QLine> GetQLinesFromNextSquares(List<KeyValuePair<Direction, Square>> directionAndNextSquares)
        {
            var qLines = new List<QLine>();

            foreach (var directionAndNextSquare in directionAndNextSquares)
            {
                var direction = directionAndNextSquare.Key;
                var qLine = _qTable.FirstOrDefault(q => q.State.Equals(_currentState) && q.Direction.Equals(direction));

                if (qLine != null)
                    qLines.Add(qLine);
            }

            return qLines;
        }

        private QLine GetQLineWithBestReward(List<QLine> qLines)
        {
            var bestReward = double.MinValue;
            QLine qLineWithBestReward = null;

            foreach (var qLine in qLines)
            {
                if (qLine.Reward > bestReward)
                {
                    bestReward = qLine.Reward;
                    qLineWithBestReward = qLine;
                }
            }

            var qLinesWithBestReward = qLines.Where(q => q.Reward == bestReward);

            if (qLinesWithBestReward.Count() > 1)
            {
                var randomIndex = Randomizer.Next(0, qLinesWithBestReward.Count());
                var randomQLine = qLinesWithBestReward.ElementAt(randomIndex);

                return randomQLine;
            }
            else
                return qLineWithBestReward;
        }

        private void UpdateQTable(KeyValuePair<Direction, Square> selectedSquareToMove)
        {
            if (_bestPathFound)
                return;

            var direction = selectedSquareToMove.Key;
            var square = selectedSquareToMove.Value;

            var qLineToUpdate = _qTable.Where(q => q.State.Equals(_currentState) && q.Direction.Equals(direction)).FirstOrDefault();

            var squaresToMove = _qTable.Where(q => q.State.Equals(square.State.Id));

            var squareWithBestReward = squaresToMove.MaxBy(s => s.Reward).FirstOrDefault();

            qLineToUpdate.Reward = square.Reward + Gamma * squareWithBestReward.Reward;
        }

        private bool VerifyIfQTableHasConverged()
        {
            bool hasQTableConverged = true;
            for (int i = 0; i < _qTable.Count; i++)
            {
                if (_qTable.ElementAt(i).Reward != _qTableAux.ElementAt(i).Reward)
                {
                    hasQTableConverged = false;
                    break;
                }
            }

            if (hasQTableConverged)
            {
                EventAggregator.GetEvent<BestPathFoundMessage>().Publish();

                return true;
            }
            else
            {
                for (int i = 0; i < _qTable.Count; i++)
                    _qTableAux.ElementAt(i).Reward = _qTable.ElementAt(i).Reward;

                return false;
            }
        }

        private ObservableCollection<QLine> InitializeQTable()
        {
            var qTable = new ObservableCollection<QLine>();
            var states = StateHelper.GetStatesAndPossibleDirections();

            foreach (var state in states)
            {
                foreach (var direction in state.PossibleDirections)
                    qTable.Add(new QLine(state.Id, direction));
            }

            return qTable;
        }
    }
}