using Microsoft.Win32;
using Next_Level.Classes;
using Next_Level.Interfaces;
using Next_Level.Pages;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Next_Level.AdminPanelPages
{
    public partial class AdminPanel : Window
    {   
        ////исходная ссылка на фото которое загружаешь
        //string sourcePhoto = String.Empty;
        ////ссылка на базу проекта
        //string target = NextLevelPath.STOREBD_PATH;
        ////ссылка на файл категорий
        //string categoryPath = NextLevelPath.CATEGORIES_PATH;
        ////для проверки условия ввода данных
        //delegate bool Conditions();

        ////список категорий
        //List<string> categories = new List<string>();
        ////продукты
        //ProductList products;
        ////интерфейс для записи и выгрузки данных
        //IFile file;
        public AdminPanel()
        {
            InitializeComponent();
            //basicSettings();
            //loadCategories();
            //loadProducts();

        }

        #region MENU_EVENTS

        private void addProductClick(object sender, RoutedEventArgs e)
        {
            panelPage.Content = null;
            panelPage.Navigate(new AddProduct());
        }

        private void editProductClick(object sender, RoutedEventArgs e)
        {
            panelPage.Content = null;
            panelPage.Navigate(new EditProduct());
        }

        private void showProductsClick(object sender, RoutedEventArgs e)
        {
            panelPage.Content = null;
            panelPage.Navigate(new ShowProducts());
        }

        private void addCategoriesClick(object sender, RoutedEventArgs e)
        {
            panelPage.Content = null;
            panelPage.Navigate(new AddCategories());
        }

        private void editCategoriesClick(object sender, RoutedEventArgs e)
        {
            panelPage.Content = null;
            panelPage.Navigate(new EditCategories());
        }

        private void addUserClick(object sender, RoutedEventArgs e)
        {
            panelPage.Content = null;
            panelPage.Navigate(new AddUser());
        }

        private void editUserClick(object sender, RoutedEventArgs e)
        {
            panelPage.Content = null;
            panelPage.Navigate(new EditUsers());
        }

        private void logOutClick(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            this.Close();
            login.ShowDialog();
        }

        #endregion

        #region WINDOW_EVENTS

        private void replacePanel(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void closePanel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void minimizePanel(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        #endregion

        
    }
}
