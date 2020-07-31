using Prism.Mvvm;
using QLearning.Enums;
using System;

namespace QLearning.Models
{
    public class QLine : BindableBase
    {
        private Direction _direction;
        private double _reward;
        private int _state;

        public Direction Direction
        {
            get => _direction;
            set => SetProperty(ref _direction, value);
        }

        public double Reward
        {
            get => _reward;
            set => SetProperty(ref _reward, Math.Round(value,2));
        }

        public int State
        {
            get => _state;
            set => SetProperty(ref _state, value);
        }

        public QLine(int state, Direction direction)
        {
            Direction = direction;
            State = state;
            Reward = 0;
        }
    }
}