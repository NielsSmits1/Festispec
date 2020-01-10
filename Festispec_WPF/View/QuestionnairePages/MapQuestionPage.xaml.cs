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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Festispec_WPF.View.QuestionnairePages
{
    /// <summary>
    /// Interaction logic for MapQuestionPage.xaml
    /// </summary>
    public partial class MapQuestionPage : Page
    {
        public MapQuestionPage()
        {
            InitializeComponent();
        }

        private void Question_LostFocus(object sender, RoutedEventArgs e)
        {
            ((Control)sender).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            question.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            if(image.Source == null)
            {
                imageError.Text = "Er is geen afbeelding geselecteerd";
                return;
            }
        }
    }
}
