using Next_Level.Classes;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;

namespace Next_Level.Pages
{

    public partial class Sign_in : Window
    {
        Accounts accounts;
        User user;
        public Sign_in()
        {
            InitializeComponent();
            accounts = new Accounts();
            user = new User();
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            cancelButton.Click += new RoutedEventHandler(cancelClick);
        }
        public bool isFieldsNoEmpty()
        {
            int count = 0;
            if (login.Text != String.Empty)
                count++;
            if (userName.Text != String.Empty)
                count++;
            if (userSurname.Text != String.Empty)
                count++;
            if (userEmail.Text != String.Empty)
                count++;
            if (userPassword.Password != String.Empty)
                count++;
            if (count != 5)
                return false;
            else return true;
        }

        public bool CheckPassword(string password)
        {
            if (EnterControl.CheckPass(password))
                return true;
            else return false;
        }

        public bool EmailCheck(string email)
        {
            if (accounts.EmailIsExist(email))
                return true;
            else return false;
        }
        
        public bool EmailCheckFormat(string email)
        {
            if (EnterControl.CheckEmail(email))
                return true;
            else return false;
        }

        public bool LoginCheck(string login)
        {
            if (accounts.LoginIsExist(login))
                return true;
            else return false;
        }

        private void createAccount_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            
            if (!isFieldsNoEmpty())
            {
                FieldsEmpty.Visibility = Visibility.Visible;
                createAccount.Margin = new Thickness(0, 0, 0, 0);
            }

            else
            {
                FieldsEmpty.Visibility = Visibility.Collapsed;
                createAccount.Margin = FieldsEmpty.Margin;
                count++;
            }

            if(!EmailCheckFormat(userEmail.Text) || EmailCheck(userEmail.Text))
            {
                if(!EmailCheckFormat(userEmail.Text))
                    EmailError2.Visibility = Visibility.Visible;
                else
                    EmailError.Visibility = Visibility.Visible;
                EmailBorder.Margin = new Thickness(0, 0, 0, 0);
                EmailBorder.BorderBrush = Brushes.Red;
            }
            else
            {
                EmailError.Visibility = Visibility.Collapsed;
                EmailError2.Visibility = Visibility.Collapsed;
                EmailBorder.Margin = EmailError.Margin;
                EmailBorder.BorderBrush = Brushes.Gray;
                count++;
            }

            if (LoginCheck(login.Text))
            {
                LoginError.Visibility = Visibility.Visible;
                LoginBorder.Margin = new Thickness(0, 0, 0, 0);
                LoginBorder.BorderBrush = Brushes.Red;
            }

            else
            {
                LoginError.Visibility = Visibility.Collapsed;
                LoginBorder.Margin = LoginError.Margin;
                LoginBorder.BorderBrush = Brushes.Gray;
                count++;
            }

            if (!CheckPassword(userPassword.Password))
            {
                PasswordError.Visibility = Visibility.Visible;
                PasswordBorder.Margin = new Thickness(0, 0, 0, 0);
                PasswordBorder.BorderBrush = Brushes.Red;
            }

            else
            {
                PasswordError.Visibility = Visibility.Collapsed;
                PasswordBorder.Margin = PasswordError.Margin;
                PasswordBorder.BorderBrush = Brushes.Gray;
                count++;
            }

            if (count >= 4)
            {
                if (login.Text == "SuperAdmin")
                    user.isAdmin = true;
                user.Login = login.Text;
                user.Name = userName.Text;
                user.Surname = userSurname.Text;
                user.Email = userEmail.Text;
                user.Password = userPassword.Password;
                accounts.AddNew(user);
                Login _login = new Login();
                this.Close();
                _login.ShowDialog();
            }
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void txtLogin_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(login.Text) && login.Text.Length > 0)
                textLogin.Visibility = Visibility.Collapsed;
            else
                textLogin.Visibility = Visibility.Visible;
            //if ((new Regex(@"^[a-zA-Z]*$")).IsMatch(Login.Text))
            //{
            //    Login.Foreground = new SolidColorBrush(Colors.White);
            //}
            //else
            //{

            //    Login.Foreground = new SolidColorBrush(Colors.Red);
            //}
        }

        private void textLogin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            login.Focus();
        }

        private void txtName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(userName.Text) && userName.Text.Length > 0)
                textName.Visibility = Visibility.Collapsed;
            else
                textName.Visibility = Visibility.Visible;
            //if ((new Regex(@"^[a-zA-Z]*$")).IsMatch(userName.Text))
            //{
            //    userName.Foreground = new SolidColorBrush(Colors.White);
            //}
            //else
            //{

            //    userName.Foreground = new SolidColorBrush(Colors.Red);
            //}
        }

        private void textName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            userName.Focus();
        }

        private void txtSurname_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(userSurname.Text) && userSurname.Text.Length > 0)
                textSurname.Visibility = Visibility.Collapsed;
            else
                textSurname.Visibility = Visibility.Visible;
            if ((new Regex(@"^[a-zA-Z]*$")).IsMatch(textSurname.Text))
            {
                textSurname.Foreground = new SolidColorBrush(Colors.White);
            }
            else
            {

                textSurname.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        private void textSurname_MouseDown(object sender, MouseButtonEventArgs e)
        {
            userSurname.Focus();
        }

        private void txtEmail_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(userEmail.Text) && userEmail.Text.Length > 0)
                textEmail.Visibility = Visibility.Collapsed;
            else
                textEmail.Visibility = Visibility.Visible;
            if ((new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")).IsMatch(textEmail.Text))
            {
                textEmail.Foreground = new SolidColorBrush(Colors.White);
            }
            else
            {
                textEmail.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            userEmail.Focus();
        }

        private void userPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(userPassword.Password) && userPassword.Password.Length > 0)
                textPassword.Visibility = Visibility.Collapsed;
            else
                textPassword.Visibility = Visibility.Visible;
            Regex ch = new Regex("^.{8,}$");
            if (ch.IsMatch(userPassword.Password))
            {
                Characters.Foreground = new SolidColorBrush(Color.FromRgb(58, 177, 155));
            }
            else
            {
                Characters.Foreground = new SolidColorBrush(Color.FromRgb(198, 0, 0));
            }
            Regex number = new Regex("^(?=.*?[0-9])");
            if (number.IsMatch(userPassword.Password))
            {
                Number.Foreground = new SolidColorBrush(Color.FromRgb(58, 177, 155));
            }
            else
            {
                Number.Foreground = new SolidColorBrush(Color.FromRgb(198, 0, 0));
            }
            Regex special = new Regex("^(?=.*?[_#?!@$%^&*-])");
            if (special.IsMatch(userPassword.Password))
            {
                Special.Foreground = new SolidColorBrush(Color.FromRgb(58, 177, 155));
            }
            else
            {
                Special.Foreground = new SolidColorBrush(Color.FromRgb(198, 0, 0));
            }
            Regex capital = new Regex("^(?=.*?[A-Z])");
            if (capital.IsMatch(userPassword.Password))
            {
                Capital.Foreground = new SolidColorBrush(Color.FromRgb(58, 177, 155));
            }
            else
            {
                Capital.Foreground = new SolidColorBrush(Color.FromRgb(198, 0, 0));
            }
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            userPassword.Focus();
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Minimize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void textPassword_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
           
        }

        private void textPassword_TextInput(object sender, TextCompositionEventArgs e)
        {
            Regex ch = new Regex("^(?=.*?[A-Z])(?=.*?[a-z]){8,}$");
            if (ch.IsMatch(textPassword.Text))
            {
                Characters.Foreground = new SolidColorBrush(Color.FromRgb(58, 177, 155));
            }
            else
            {
                Characters.Foreground = new SolidColorBrush(Color.FromRgb(198, 0, 0));
            }
            Regex number = new Regex("^(?=.*?[0-9])");
            if (number.IsMatch(textPassword.Text))
            {
                Number.Foreground = new SolidColorBrush(Color.FromRgb(58, 177, 155));
            }
            else
            {
                Number.Foreground = new SolidColorBrush(Color.FromRgb(198, 0, 0));
            }
            Regex special = new Regex("^(?=.*?[_#?!@$%^&*-])");
            if (special.IsMatch(textPassword.Text))
            {
                Special.Foreground = new SolidColorBrush(Color.FromRgb(58, 177, 155));
            }
            else
            {
                Special.Foreground = new SolidColorBrush(Color.FromRgb(198, 0, 0));
            }
            Regex capital = new Regex("^(?=.*?[A-Z])");
            if (capital.IsMatch(textPassword.Text))
            {
                Capital.Foreground = new SolidColorBrush(Color.FromRgb(58, 177, 155));
            }
            else
            {
                Capital.Foreground = new SolidColorBrush(Color.FromRgb(198, 0, 0));
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void cancelClick(object sender,RoutedEventArgs e)
        {
            Login _login = new Login();
            this.Close();
            _login.ShowDialog();
        }
    }
}









