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
            if ((new Regex(@"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|6(?:011|5[0-9]{2})[0-9]{12}(?:2131|1800|35\d{3})\d{11})$")).IsMatch(NumberCard.Text)) // проверка карты только для виза у визы начиется с 4 номер
            {
                NumberCard.Foreground = new SolidColorBrush(Colors.White);
            }
            else
            {
                NumberCard.Foreground = new SolidColorBrush(Colors.Red);
                
            }
            if(string.IsNullOrEmpty(NumberCard.Text))
            {
                NumberCard.BorderBrush = new SolidColorBrush(Colors.Red);
            }
        }

        private void DateCard_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void CVVCard_TextChanged(object sender, TextChangedEventArgs e)
        {
            
             if ((new Regex(@"^([0-9]{3,4})$")).IsMatch(CVVCard.Text)) // проверка CVV
            {
                CVVCard.Foreground = new SolidColorBrush(Colors.White);
            }
            else
            {
                CVVCard.Foreground = new SolidColorBrush(Colors.Red);


            }
            if (string.IsNullOrEmpty(NumberCard.Text))
            {
                CVVCard.BorderBrush = new SolidColorBrush(Colors.Red);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((new Regex(@"^(0[1-9]|1[0-2])\/?([0-9]{4}|[0-9]{2})$")).IsMatch(DateCard.Text)) // проверка даты ввод(0524 или 05/24)
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

        private void btnMaster_Click(object sender, RoutedEventArgs e)
        {
            
            if ((new Regex(@"^(5[1-5][0-9]{14}|2(22[1-9][0-9]{12}|2[3-9][0-9]{13}|[3-6][0-9]{14}|7[0-1][0-9]{13}|720[0-9]{12}))$")).IsMatch(DateCard.Text)) // проверка мастеркада там начинается ввод с 51 до 54
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
