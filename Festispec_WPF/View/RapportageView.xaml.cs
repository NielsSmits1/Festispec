using FestiSpec.Domain.Model;
using Festispec_WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Festispec_WPF.View
{
    /// <summary>
    /// Interaction logic for RapportageView.xaml
    /// </summary>
    public partial class RapportageView : Window
    {
        public RapportageView(Inspectie Inspection)
        {
            InitializeComponent();
            ((RapportageVM)DataContext).selectedInspection = Inspection;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((RapportageVM)DataContext).loaded = true;
        }
    }
}
