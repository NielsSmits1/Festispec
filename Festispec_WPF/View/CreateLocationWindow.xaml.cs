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
    /// Interaction logic for CreateLocationWindow.xaml
    /// </summary>
    public partial class CreateLocationWindow : Window
    {
        public CreateLocationWindow()
        {
            InitializeComponent();
        }


        private void Postcode_LostFocus(object sender, RoutedEventArgs e)
        {
            ((Control)sender).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void Huisnummer_LostFocus(object sender, RoutedEventArgs e)
        {
            ((Control)sender).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void Straatnaam_LostFocus(object sender, RoutedEventArgs e)
        {
            ((Control)sender).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Postcode.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            Huisnummer.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            Straatnaam.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }
    }
}
