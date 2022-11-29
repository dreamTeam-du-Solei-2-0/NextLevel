using Next_Level.Classes;
using Next_Level.Interfaces;
using System;
using System.Collections.Generic;
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
    public partial class Users : Page
    {
        public string current_user { get; set; }
        string path_currentUser = @"..\Debug\CurrentLogin.bin";
        IFile file;
        Accounts accounts = new Accounts();
        User user;
        public Users()
        {
            InitializeComponent();
            file = new BinnaryFile(path_currentUser);
            current_user = file.Load<string>();
            this.user = accounts.getUserByLogin(current_user);
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            login.Text = user.Login;
            name.Text = user.Name;
            Surname.Text = user.Surname;
            email.Text = user.Email;
            Password.Password = user.Password;
        }

        private void changeData_Click(object sender, RoutedEventArgs e)
        {
            accounts.removeUser(user);
            if(!accounts.LoginIsExist(login.Text))
                user.Login = login.Text;
            user.Name= name.Text;
            user.Surname=Surname.Text ;
            if (!accounts.EmailIsExist(email.Text))
                user.Email = email.Text;
            user.Password=Password.Password;
            accounts.AddNew(user);
        }
        private void txtLogin_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(login.Text) && login.Text.Length > 0)
                textLogin.Visibility = Visibility.Collapsed;
            else
                textLogin.Visibility = Visibility.Visible;
        }

        private void textLogin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            login.Focus();
        }

        private void txtName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(name.Text) && name.Text.Length > 0)
                textName.Visibility = Visibility.Collapsed;
            else
                textName.Visibility = Visibility.Visible;
        }

        private void textName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            name.Focus();
        }

        private void txtSurname_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Surname.Text) && Surname.Text.Length > 0)
                textSurname.Visibility = Visibility.Collapsed;
            else
                textSurname.Visibility = Visibility.Visible;
        }

        private void textSurname_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Surname.Focus();
        }

        private void txtEmail_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(email.Text) && email.Text.Length > 0)
                textEmail.Visibility = Visibility.Collapsed;
            else
                textEmail.Visibility = Visibility.Visible;
        }

        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            email.Focus();
        }

        private void txtPhone_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(email.Text) && email.Text.Length > 0)
                textPhone.Visibility = Visibility.Collapsed;
            else
                textPhone.Visibility = Visibility.Visible;
        }

        private void textPhone_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Phone.Focus();
        }

        private void txtBirth_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(email.Text) && email.Text.Length > 0)
                textBirth.Visibility = Visibility.Collapsed;
            else
                textBirth.Visibility = Visibility.Visible;
        }

        private void textBirth_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Birthday.Focus();
        }

        private void userPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Password.Password) && Password.Password.Length > 0)
                textPassword.Visibility = Visibility.Collapsed;
            else
                textPassword.Visibility = Visibility.Visible;
            Regex ch = new Regex("^.{8,}$");
            if (ch.IsMatch(Password.Password))
            {
                Characters.Foreground = new SolidColorBrush(Color.FromRgb(58, 177, 155));
            }
            else
            {
                Characters.Foreground = new SolidColorBrush(Color.FromRgb(198, 0, 0));
            }
            Regex number = new Regex("^(?=.*?[0-9])");
            if (number.IsMatch(Password.Password))
            {
                Number.Foreground = new SolidColorBrush(Color.FromRgb(58, 177, 155));
            }
            else
            {
                Number.Foreground = new SolidColorBrush(Color.FromRgb(198, 0, 0));
            }
            Regex special = new Regex("^(?=.*?[_#?!@$%^&*-])");
            if (special.IsMatch(Password.Password))
            {
                Special.Foreground = new SolidColorBrush(Color.FromRgb(58, 177, 155));
            }
            else
            {
                Special.Foreground = new SolidColorBrush(Color.FromRgb(198, 0, 0));
            }
            Regex capital = new Regex("^(?=.*?[A-Z])");
            if (capital.IsMatch(Password.Password))
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
            Password.Focus();
        }

        //fields_check
        public bool isFieldsNoEmpty()
        {
            int count = 0;
            if (login.Text != String.Empty)
                count++;
            if (name.Text != String.Empty)
                count++;
            if (Surname.Text != String.Empty)
                count++;
            if (email.Text != String.Empty)
                count++;
            if (Password.Password != String.Empty)
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

        //private void createAccount_Click(object sender, RoutedEventArgs e)
        //{
        //    int count = 0;

        //    if (!isFieldsNoEmpty())
        //    {
        //        FieldsEmpty.Visibility = Visibility.Visible;
        //        createAccount.Margin = new Thickness(0, 0, 0, 0);
        //    }

        //    else
        //    {
        //        FieldsEmpty.Visibility = Visibility.Collapsed;
        //        createAccount.Margin = FieldsEmpty.Margin;
        //        count++;
        //    }

        //    if (!EmailCheckFormat(userEmail.Text) || EmailCheck(userEmail.Text))
        //    {
        //        if (!EmailCheckFormat(userEmail.Text))
        //            EmailError2.Visibility = Visibility.Visible;
        //        else
        //            EmailError.Visibility = Visibility.Visible;
        //        EmailBorder.Margin = new Thickness(0, 0, 0, 0);
        //        EmailBorder.BorderBrush = Brushes.Red;
        //    }
        //    else
        //    {
        //        EmailError.Visibility = Visibility.Collapsed;
        //        EmailError2.Visibility = Visibility.Collapsed;
        //        EmailBorder.Margin = EmailError.Margin;
        //        EmailBorder.BorderBrush = Brushes.Gray;
        //        count++;
        //    }

        //    if (LoginCheck(Login.Text))
        //    {
        //        LoginError.Visibility = Visibility.Visible;
        //        LoginBorder.Margin = new Thickness(0, 0, 0, 0);
        //        LoginBorder.BorderBrush = Brushes.Red;
        //    }

        //    else
        //    {
        //        LoginError.Visibility = Visibility.Collapsed;
        //        LoginBorder.Margin = LoginError.Margin;
        //        LoginBorder.BorderBrush = Brushes.Gray;
        //        count++;
        //    }

        //    if (!CheckPassword(userPassword.Password))
        //    {
        //        PasswordError.Visibility = Visibility.Visible;
        //        PasswordBorder.Margin = new Thickness(0, 0, 0, 0);
        //        PasswordBorder.BorderBrush = Brushes.Red;
        //    }

        //    else
        //    {
        //        PasswordError.Visibility = Visibility.Collapsed;
        //        PasswordBorder.Margin = PasswordError.Margin;
        //        PasswordBorder.BorderBrush = Brushes.Gray;
        //        count++;
        //    }

        //    if (count >= 4)
        //    {
        //        user.Login = login.Text;
        //        user.Name = name.Text;
        //        user.Surname = Surname.Text;
        //        user.Email = email.Text;
        //        user.Password = Password.Password;
        //        accounts.AddNew(user);
        //    }
        //}
    }
}
