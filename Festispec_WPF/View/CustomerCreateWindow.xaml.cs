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
    /// Interaction logic for CustomerCreateWindow.xaml
    /// </summary>
    public partial class CustomerCreateWindow : Window
    {
        public CustomerCreateWindow()
        {
            InitializeComponent();
        }

        private void FirstName_LostFocus(object sender, RoutedEventArgs e)
        {
            ((Control)sender).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void Prefix_LostFocus(object sender, RoutedEventArgs e)
        {
            ((Control)sender).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void LastName_LostFocus(object sender, RoutedEventArgs e)
        {
            ((Control)sender).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void Mail_LostFocus(object sender, RoutedEventArgs e)
        {
            ((Control)sender).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void Number_LostFocus(object sender, RoutedEventArgs e)
        {
            ((Control)sender).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void Company_LostFocus(object sender, RoutedEventArgs e)
        {
            ((Control)sender).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void Location_LostFocus(object sender, RoutedEventArgs e)
        {
            ((Control)sender).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void Postalcode_LostFocus(object sender, RoutedEventArgs e)
        {
            ((Control)sender).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void Housenumber_LostFocus(object sender, RoutedEventArgs e)
        {
            ((Control)sender).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void StreetName_LostFocus(object sender, RoutedEventArgs e)
        {
            ((Control)sender).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void KVKNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            ((Control)sender).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void IBAN_LostFocus(object sender, RoutedEventArgs e)
        {
            ((Control)sender).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            firstName.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            lastName.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            prefix.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            mail.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            number.GetBindingExpression(TextBox.TextProperty).UpdateSource();

            company.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            location.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            postalcode.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            housenumber.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            streetName.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            KVKNumber.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            IBAN.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }
    }
}
