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
    /// Interaction logic for CreateInspectionWindow.xaml
    /// </summary>
    public partial class CreateInspectionWindow : Window
    {
        public CreateInspectionWindow()
        {
            InitializeComponent();
        }

        private void Title_LostFocus(object sender, RoutedEventArgs e)
        {
            ((Control)sender).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Title.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            Location.GetBindingExpression(ListBox.SelectedItemProperty).UpdateSource();
            customer.GetBindingExpression(ComboBox.SelectedItemProperty).UpdateSource();
        }
    }
}
