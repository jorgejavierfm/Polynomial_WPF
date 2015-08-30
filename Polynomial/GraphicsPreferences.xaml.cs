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
using System.Windows.Shapes;
using WPFColorPickerLib;

namespace Polynomial
{
    /// <summary>
    /// Interaction logic for GraphicsPreferences.xaml
    /// </summary>
    public partial class GraphicsPreferences : Window
    {
        public GraphicsPreferences()
        {
            InitializeComponent();
        }

        public double xMin
        {
            get
            {
                return Convert.ToDouble(_xMin.Text);
            }
            set
            { _xMin.Text = value.ToString(); }
        }
        public double xMax
        {
            get
            {
                return Convert.ToDouble(_xMax.Text);
            }
            set
            {
                _xMax.Text = value.ToString();
            }
        }
        public double yMin
        {
            get
            { return Convert.ToDouble(_yMin.Text); }
            set
            { _yMin.Text = value.ToString(); }
        }
        public double yMax
        {
            get
            { return Convert.ToDouble(_yMax.Text); }
            set
            { _yMax.Text = value.ToString(); }
        }
        public double markIntervale
        {
            get
            { return Convert.ToDouble(_markIntervale.Text); }
            set
            { _markIntervale.Text = value.ToString(); }
        }
        public Color color
        {
            get
            {
                return ((SolidColorBrush)rectColor.Fill).Color;
            }
            set
            {
                rectColor.Fill = new SolidColorBrush(value);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.SelectedColor = ((SolidColorBrush)this.rectColor.Fill).Color;
            colorDialog.Owner = this;

            var current = this.Background;
            this.Background = new SolidColorBrush(Colors.LightGray);
            this.Effect = new BlurEffect();

            if ((bool)colorDialog.ShowDialog())
                rectColor.Fill = new SolidColorBrush(colorDialog.SelectedColor);

            this.Effect = null;
            this.Background = current;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (_xMin.Text == "" || _xMax.Text == "" || _yMin.Text == "" || _yMax.Text == "" || _markIntervale.Text=="")
                MessageBox.Show("Please fill the remaining fields", "New Function", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else if (Convert.ToDouble(xMin)>0 || Convert.ToDouble(yMin)>0)
                MessageBox.Show("The minimum value of x or y cannot be greater than 0", "New Function", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
            DialogResult = true;
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }


    }
}
