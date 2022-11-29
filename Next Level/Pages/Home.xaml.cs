using Next_Level.Classes;
using Next_Level.Interfaces;
using System;
using System.Collections;
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

namespace Next_Level.Pages
{
    public partial class Home : Page
    {
        public string current_user { get; set; }
        string path_currentUser = @"..\Debug\CurrentLogin.bin";
        IFile file;
        
        public Home()
        {
            InitializeComponent();
            file = new BinnaryFile(path_currentUser);
            this.current_user = file.Load<string>();
            var products = GetProducts(21);
            if (products.Count > 0)
                ListViewProducts.ItemsSource = products;
        }

        private List<Product> GetProducts(int number)
        {
            var products = new List<Product>();
            for (int i = 1; i <= number; i++)
            {
                products.Add(new Product($"Товар {i}", i, $"Assets\\Images\\{i}.png"));

               
            }
            return products;
        }

        private void test_Click(object sender, RoutedEventArgs e)
        {
            ProductInfo productInfo = new ProductInfo();
            productInfo.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProductInfo productInfo = new ProductInfo();
            productInfo.ShowDialog();
        }
    }

}