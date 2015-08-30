using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Polynomial.Clases
{
    public class Polynomial
    {
        public Polynomial()
        {
            name = "";
            function = "";
            drawingColor = Colors.Black;
            plot = new Polyline();
            xMin = -19;
            xMax = 19;
            yMin = -19;
            yMax = 19;
        }

        public String name { get; set; }
        public String function { get; set; }
        public Color drawingColor { get; set; }
        public Polyline plot { get; set; }
        public double xMin { get; set; }
        public double xMax { get; set; }
        public double yMin { get; set; }
        public double yMax { get; set; }

    }
}
