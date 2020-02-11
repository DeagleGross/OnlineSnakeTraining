using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace SafeboardSnake.WPF.Models
{
    public class ColorProvider
    {
        public static Brush SnakeColor => Brushes.Chocolate;
        public static Brush BackgroundColorOdd => new SolidColorBrush(Color.FromArgb(255, 240, 248, 251));
        public static Brush BackgroundColorEven => new SolidColorBrush(Color.FromArgb(255, 214, 236, 255));
        public static Brush FoodColor => Brushes.Brown;
        public static Brush WallColor => Brushes.SlateGray;
        public static Brush UserSnakeColor => Brushes.DeepPink;
    }
}
