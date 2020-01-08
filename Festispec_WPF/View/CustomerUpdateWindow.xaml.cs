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
    /// Interaction logic for CustomerUpdateWindow.xaml
    /// </summary>
    public partial class CustomerUpdateWindow : Window
    {
        public CustomerUpdateWindow()
        {
            InitializeComponent();
        }

        private void Firstname_LostFocus(object sender, RoutedEventArgs e)
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

        private void Number_LostFocus(object sender, RoutedEventArgs e)
        {
            ((Control)sender).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void Mail_LostFocus(object sender, RoutedEventArgs e)
        {
            ((Control)sender).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            firstname.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            prefix.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            lastName.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            number.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            mail.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }
    }
}
