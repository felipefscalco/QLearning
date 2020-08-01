using Prism.Mvvm;
using System.Windows.Media;

namespace QLearning.Models
{
    public class Square : BindableBase
    {
        private Brush _color;

        public int X { get; }

        public int Y { get; }

        public int Width { get; }

        public int Height { get; }

        public Brush Color
        {
            get => _color;
            private set => SetProperty(ref _color, value);
        }

        public double Reward { get; }

        public State State { get; }

        public Square(int x, int y, int width, int height, Brush color, double reward, State state)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Color = color;
            Reward = reward;
            State = state;
        }

        public void SetColor(Brush color)
        {
            Color = color;
            Color.Freeze();
        }

        public int GetRow()
            => X / Height;

        public int GetColumn()
            => Y / Width;
    }
}