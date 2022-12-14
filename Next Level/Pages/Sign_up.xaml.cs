using Next_Level.AdminPanelPages;
using Next_Level.Classes;
using Next_Level.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Next_Level.Pages
{
    
    public partial class Login : Window
    {
        string path_saveData = NextLevelPath.SAVE_LOGIN;
        string path_currentUser = NextLevelPath.CURRENT_USER;
        Accounts account;
        IFile file;
        SaveLogin saveLogin;
        bool isSaved;
        public Login()
        {
            InitializeComponent();
            isSaved = false;
            account = new Accounts();
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            if (File.Exists(path_saveData))
            {
                file = new BinnaryFile(path_saveData);
                saveLogin = file.Load<SaveLogin>();
                saveLog.IsChecked = true;
                txtEmail.Text = saveLogin.login;
                passwordBox.Password = saveLogin.password;
            }
            else
                saveLogin = new SaveLogin();
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(passwordBox.Password) && passwordBox.Password.Length > 0)
                textPassword.Visibility = Visibility.Collapsed;
            else
                textPassword.Visibility = Visibility.Visible;
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            passwordBox.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;

            if (account.LoginIsExist(txtEmail.Text))
            {
                loginBorder.BorderBrush = Brushes.Gray;
                loginBorder.Margin = new Thickness(0, 0, 0, 20);
                LoginError.Visibility = Visibility.Collapsed;
                count++;
            }
            else
            {
                loginBorder.BorderBrush = Brushes.Red;
                loginBorder.Margin = new Thickness(0, 0, 0, 0);
                LoginError.Visibility = Visibility.Visible;
            }

            if (account.CheckPassword(txtEmail.Text, passwordBox.Password))
            {
                passwordBorder.BorderBrush = Brushes.Gray;
                PasswordError.Visibility = Visibility.Collapsed;
                count++;
            }
            else
            {
                passwordBorder.BorderBrush = Brushes.Red;
                PasswordError.Visibility = Visibility.Visible;
            }

            if(count == 2)
            {
                if(isSaved)
                {
                    if (txtEmail.Text != String.Empty && passwordBox.Password != String.Empty)
                    {
                        file = new BinnaryFile(path_saveData);
                        saveLogin = new SaveLogin();
                        saveLogin.save_Data = isSaved;
                        saveLogin.login = txtEmail.Text;
                        saveLogin.password = passwordBox.Password;
                        file.Save(saveLogin);
                    }
                }
                if (saveLog.IsChecked == false)
                {
                    File.Delete(path_saveData);
                }
                if (txtEmail.Text == "SuperAdmin")
                {
                    AdminPanel admin = new AdminPanel();
                    this.Close();
                    admin.Show();
                }
                else
                {
                    MainWindow main = new MainWindow();
                    file = new BinnaryFile(path_currentUser);
                    file.Save(txtEmail.Text);
                    this.Close();
                    main.Show();
                }
            }
            
        }

        private void txtEmail_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && txtEmail.Text.Length > 0)
                textEmail.Visibility = Visibility.Collapsed;
            else
                textEmail.Visibility = Visibility.Visible;
        }

        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtEmail.Focus();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Sign_in sign_In = new Sign_in();
            this.Close();
            sign_In.ShowDialog();

        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Minimize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void saveLoginData(object sender, RoutedEventArgs e)
        {
            isSaved = true;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
