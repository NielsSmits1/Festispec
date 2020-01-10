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
    /// Interaction logic for QuestionnaireWindow.xaml
    /// </summary>
    public partial class EditQuestionnaireWindow : Window
    {
        public EditQuestionnaireWindow()
        {
            InitializeComponent();
        }

        private void Title_LostFocus(object sender, RoutedEventArgs e)
        {
            ((Control)sender).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void Version_LostFocus(object sender, RoutedEventArgs e)
        {
            ((Control)sender).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void Note_LostFocus(object sender, RoutedEventArgs e)
        {
            ((Control)sender).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            version.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            title.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            note.GetBindingExpression(TextBox.TextProperty).UpdateSource();

            if (orderGrid.Items.Count == 0)
            {
                errorText.Text = "Vragenlijst moet minimaal 1 vraag bevatten";
                return;
            }
            else
            {
                errorText.Text = "";
            }
        }
    }
}
