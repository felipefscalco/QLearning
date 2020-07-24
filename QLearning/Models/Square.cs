using QLearning.Enums;
using System.Collections.Generic;
using System.Windows.Media;

namespace QLearning.Models
{
    public class Square
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public Brush Color { get; set; }

        public string State { get; set; }

        public List<Direction> PossibleDirections { get; set; }

        public double Reward { get; set; }

        public int IndexAtList { get; set; }
    }
}