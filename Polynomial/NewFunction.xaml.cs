using System;
using System.Globalization;
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
    /// Interaction logic for NewFunction.xaml
    /// </summary>
    public partial class NewFunction : Window
    {
        public NewFunction()
        {
            InitializeComponent();
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

        public String newFunction
        {
            get
            {
                return function.Text;
            }
            set
            {
                function.Text = value;
            }
        }
        public String nameFunction 
        {
            get 
            {
                return functionName.Text;
            }
            set
            {
                functionName.Text = value;
            }
        }
        public Color color 
        {
            get
            {
                return ((SolidColorBrush)rectColor.Fill).Color;
            }
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
                return Convert.ToDouble(_xMax.Text); }
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            if (functionName.Text == "" || function.Text=="" || _xMin.Text==""  || _xMax.Text=="" || _yMin.Text=="" || _yMax.Text=="")
                MessageBox.Show("Please fill the remaining fields", "New Function", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else if((parameterA.IsEnabled==true && parameterA.Text=="") || (parameterB.IsEnabled==true && parameterB.Text=="") || (parameterC.IsEnabled==true && parameterC.Text=="") ||(parameterN.IsEnabled==true && parameterN.Text==""))
                 MessageBox.Show("Please fill the remaining fields", "New Function", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
                DialogResult = true;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            parameterA.IsEnabled = true;
            parameterB.IsEnabled = true;
        }
        private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            parameterA.IsEnabled = true;
            parameterN.IsEnabled = true;
        }
        private void ComboBoxItem_Selected_2(object sender, RoutedEventArgs e)
        {
            parameterA.IsEnabled = true;
            parameterB.IsEnabled = true;
            parameterC.IsEnabled = true;
        }
        private void ComboBoxItem_Unselected(object sender, RoutedEventArgs e)
        {
            parameterA.IsEnabled = false;
            parameterB.IsEnabled = false;
            parameterC.IsEnabled = false;
            parameterN.IsEnabled = false;
        }
    }
}
