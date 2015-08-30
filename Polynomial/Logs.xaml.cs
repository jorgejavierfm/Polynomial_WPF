using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Polynomial;

namespace Polynomial
{
    /// <summary>
    /// Interaction logic for Logs.xaml
    /// </summary>
    public partial class Logs : Window
    {
        ObservableCollection<Clases.Polynomial> functionsLogs;

        public Logs(ObservableCollection<Clases.Polynomial> functions)
        {
            InitializeComponent();
            datagrid.ItemsSource = functions;
            functionsLogs = functions;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            functionsLogs.RemoveAt(datagrid.SelectedIndex);
            remove.IsEnabled = false;
        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            remove.IsEnabled = true;
        }
    }
}
