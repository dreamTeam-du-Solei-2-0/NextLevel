using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Next_Level.Pages
{
    public partial class Cart : Page
    {
        public Cart()
        {
            InitializeComponent();
        }

        private void StackPanel_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
               
            }
        }

        private void NumberCard_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((new Regex(@"[^0-9.-]+")).IsMatch(NumberCard.Text))
            {
                NumberCard.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                NumberCard.Foreground = new SolidColorBrush(Colors.White);
                
            }
            if(string.IsNullOrEmpty(NumberCard.Text))
            {
                NumberCard.BorderBrush = new SolidColorBrush(Colors.Red);
            }
        }

        private void DateCard_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((new Regex(@"^(0[13578]|1[02])/([012]\d|3[01])|(0[469]|11)/([012]\d|30)|02/[012]\d$")).IsMatch(DateCard.Text))
            {
                DateCard.Foreground = new SolidColorBrush(Colors.White);
            }
            else
            {
                DateCard.Foreground = new SolidColorBrush(Colors.Red);
               

            }
            if (string.IsNullOrEmpty(NumberCard.Text))
            {
                DateCard.BorderBrush = new SolidColorBrush(Colors.Red);
            }
        }
    }
}
