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
using Next_Level.Themes;
using Next_Level.Pages;
using System.Windows.Controls.Primitives;
using Next_Level.Interfaces;
using Next_Level.Classes;

namespace Next_Level
{
    public partial class MainWindow : Window
    {
        public string current_user { get; set; }
        string path_currentUser = NextLevelPath.CURRENT_USER;
        IFile file;
        public MainWindow()
        {
            InitializeComponent();
            file = new BinnaryFile(path_currentUser);
            current_user = file.Load<string>();
            frameContent.Navigate(new Home());
            rdHome.IsChecked = true;
        }

        private void headerThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Left = Left + e.HorizontalChange;
            Top = Top + e.VerticalChange;
        }

        private void Themes_Click(object sender, RoutedEventArgs e)
        {
            if (Themes.IsChecked == true)
            {
                ThemesController.SetTheme(ThemesController.ThemeTypes.Dark);
                frameContent.Navigate(new Home());
                rdHome.IsChecked = true;
            }
            else
            {
                ThemesController.SetTheme(ThemesController.ThemeTypes.Light);
                frameContent.Navigate(new Home());
                rdHome.IsChecked = true;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
                
            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void rdHome_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new Home());
        }

        private void rdCart_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new Cart());
        }

        private void rdMessages_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new Messages());
        }

        private void rdCollections_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new Collections());
        }

        private void rdUsers_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new Users());
        }

        private void logOut_Click(object sender, RoutedEventArgs e)
        {
            Login sign_In = new Login();
            this.Close();
            sign_In.Show();
        }
    }
}
