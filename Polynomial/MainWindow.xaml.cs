using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Effects;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Polynomial.Clases;

namespace Polynomial
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double xMin = -20;
        double xMax = 20;
        double yMin = -20;
        double yMax = 20;
        double markIntervale = 2;
        Color backgroundColor = Colors.White;

        PolynomialsController controller;
        ObservableCollection<Clases.Polynomial> functions;
        
        public MainWindow()
        {
            InitializeComponent();

            controller = new PolynomialsController();
            functions = controller.functions;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            Rectangle area = new Rectangle();
            area.Width = Math.Abs(xMin)+xMax;
            area.Height = Math.Abs(yMin)+yMax;

            canvas.Children.Clear();

            MatrixTransform mirror = new MatrixTransform(1, 0, 0, -1, 0, 0);
            TranslateTransform translate = new TranslateTransform(canvas.ActualWidth / (area.Width / Math.Abs(xMin)), canvas.ActualHeight / (area.Height/(yMax)));
            ScaleTransform scale = new ScaleTransform(canvas.ActualWidth/area.Width, canvas.ActualHeight / area.Height);
            
            TransformGroup newAxisSystem = new TransformGroup();
            newAxisSystem.Children.Add(mirror);
            newAxisSystem.Children.Add(scale);
            newAxisSystem.Children.Add(translate);

            area.Fill = new SolidColorBrush(backgroundColor);
            area.RenderTransform = scale;

            canvas.Children.Add(area);

            DrawAxis(newAxisSystem,area,markIntervale);

            if (functions != null)
                foreach (Clases.Polynomial p in functions)
                {
                    p.plot.Stroke = new SolidColorBrush(p.drawingColor);
                    p.plot.StrokeThickness = 0.1;
                    p.plot.RenderTransform = newAxisSystem;
                    canvas.Children.Add(p.plot);
                }
        }

        private void DrawAxis(TransformGroup axisSystem,Rectangle area,double markIntervale)
        {
            Line xAxis = new Line();
            Line yAxis = new Line();
            xAxis.Stroke = Brushes.Black;
            xAxis.StrokeThickness = 0.1;
            yAxis.Stroke = Brushes.Black;
            yAxis.StrokeThickness = 0.1;

            yAxis.X1 = 0;
            yAxis.Y1 = area.Height;

            yAxis.X2 = 0;
            yAxis.Y2 = -1*yAxis.Y1;

            yAxis.RenderTransform = axisSystem;
            canvas.Children.Add(yAxis);

            Point mark = new Point();
            mark.X = 0;
            mark.Y = 0;

            for (int i = 0; i < (area.Height) / markIntervale; i++)
            {
                mark.Y += markIntervale;
                markAux(mark, axisSystem, 1);
            }

            mark.X = 0;
            mark.Y = 0;

            for (int i = 0; i < (area.Height) / markIntervale; i++)
            {
                mark.Y -= markIntervale;
                markAux(mark, axisSystem, 1);
            }

            xAxis.X1 = area.Width;
            xAxis.Y1 = 0;

            xAxis.X2 = -1*xAxis.X1;
            xAxis.Y2 = 0;

            xAxis.RenderTransform = axisSystem;
            canvas.Children.Add(xAxis);

            mark.X = 0;
            mark.Y = 0;

            for (int i = 0; i < (area.Width) / markIntervale; i++)
            {
                mark.X += markIntervale;
                markAux(mark, axisSystem,0);
            }

            mark.X = 0;
            mark.Y = 0;

            for (int i = 0; i < (area.Width) / markIntervale; i++)
            {
                mark.X -= markIntervale;
                markAux(mark, axisSystem,0);
            }
        }
        private void markAux(Point mark,TransformGroup axisSystem,int eje)
        {
            Line markLine = new Line();
            markLine.Stroke = Brushes.Black;
            markLine.StrokeThickness = 0.03;

            if (eje == 0)
            {
                markLine.X1 = mark.X;
                markLine.Y1 = 0.5;
                markLine.X2 = mark.X;
                markLine.Y2 = -1 * markLine.Y1;
            }
            else
            {
                markLine.Y1 = mark.Y;
                markLine.X1 = 0.5;
                markLine.Y2 = mark.Y;
                markLine.X2 = -1 * markLine.X1;
            }

            markLine.RenderTransform = axisSystem;
            canvas.Children.Add(markLine);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool returnValue;

            NewFunction functionWindow = new NewFunction();
            functionWindow.Owner = this;

            var current = this.Background;
            this.Background = new SolidColorBrush(Colors.LightGray);
            this.Effect = new BlurEffect();

            returnValue = controller.addFunction(functionWindow);

            this.Effect = null;
            this.Background = current;

            if (returnValue)
                Refresh();

        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GraphicsPreferences preferences = new GraphicsPreferences();
            preferences.Owner = this;

            preferences.xMin = xMin;
            preferences.xMax = xMax;
            preferences.yMin = yMin;
            preferences.yMax = yMax;
            preferences.markIntervale = markIntervale;
            preferences.color = backgroundColor;

            var current = this.Background;
            this.Background = new SolidColorBrush(Colors.LightGray);
            this.Effect = new BlurEffect();

            if ((bool)preferences.ShowDialog())
            {
                xMin = preferences.xMin;
                xMax = preferences.xMax;
                yMin = preferences.yMin;
                yMax = preferences.yMax;
                markIntervale = preferences.markIntervale;
                backgroundColor = preferences.color;
                Refresh();
            }

            this.Effect = null;
            this.Background = current;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Logs logs = new Logs(functions);
            logs.Owner = this;

         
            var current = this.Background;
            this.Background = new SolidColorBrush(Colors.LightGray);
            this.Effect = new BlurEffect();

            logs.ShowDialog();

            this.Effect = null;
            this.Background = current;

            Refresh();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Develop by: Jorge Javier\nContact: jorgejavierfm@usal.es", "Main Window", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
