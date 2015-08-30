using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Collections.ObjectModel;
using MathParserNet;

namespace Polynomial.Clases
{
    class PolynomialsController
    {
        public PolynomialsController()
        {
            functions = new ObservableCollection<Polynomial>();
        }

        public PointCollection calculatePointCollection(Polynomial function,String[] parameters)
        {
            Parser parser = new Parser();

            if (parameters[0] != "-")
                parser.AddVariable("a",Double.Parse(parameters[0]));

            if (parameters[1] != "-")
                parser.AddVariable("b", Double.Parse(parameters[1]));

            if (parameters[2] != "-")
                parser.AddVariable("c", Double.Parse(parameters[2]));

            if (parameters[3] != "-")
                parser.AddVariable("n", Double.Parse(parameters[3]));

            PointCollection pathPoints = new PointCollection();
            Point point=new Point();

            double functionWidth = (Math.Abs(function.xMin) + function.xMax);
            double distance=functionWidth/500;

            point.X = function.xMin;

            while (point.X <= function.xMin + functionWidth)
            {
                parser.AddVariable("x",point.X);
                try
                {
                    point.Y = parser.SimplifyDouble(function.function);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "New Function", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    break;
                }

                if(point.Y>=function.yMin && point.Y<=function.yMax)
                    pathPoints.Add(point);
                point.X += distance;
                parser.RemoveVariable("x");
            }

            return pathPoints;
        }
        public bool addFunction(NewFunction windowToShow)
        {
            String[] parameters = new String[4];
            Polynomial newFunction = new Polynomial();

            if ((bool)windowToShow.ShowDialog())
            {
                newFunction.name = windowToShow.nameFunction;
                newFunction.function = windowToShow.newFunction;
                newFunction.drawingColor = windowToShow.color;
                newFunction.xMin = windowToShow.xMin;
                newFunction.xMax = windowToShow.xMax;
                newFunction.yMin = windowToShow.yMin;
                newFunction.yMax = windowToShow.yMax;

                if (windowToShow.parameterA.IsEnabled)
                    parameters[0] = windowToShow.parameterA.Text;
                else
                    parameters[0] = "-";

                if (windowToShow.parameterB.IsEnabled)
                    parameters[1] = windowToShow.parameterB.Text;
                else
                    parameters[1] = "-";

                if (windowToShow.parameterC.IsEnabled)
                    parameters[2] = windowToShow.parameterC.Text;
                else
                    parameters[2] = "-";

                if (windowToShow.parameterN.IsEnabled)
                    parameters[3] = windowToShow.parameterN.Text;
                else
                    parameters[3] = "-";

                newFunction.plot.Points = calculatePointCollection(newFunction, parameters);
                if (newFunction.plot.Points.Count != 0)
                {
                    functions.Add(newFunction);
                    return true;
                }
                else
                {
                    MessageBox.Show("Could not calculate the function", "New Function", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return false;
                }
            }

            return false;
        }
        public ObservableCollection<Polynomial> functions { get; set; }
    }
}
