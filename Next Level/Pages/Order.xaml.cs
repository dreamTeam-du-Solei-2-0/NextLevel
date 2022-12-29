using Next_Level.Classes;
using Next_Level.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Next_Level.Pages
{
    public partial class Order : Page
    {
        Home relative_page;
     
        User current_user;
        Accounts accounts = new Accounts();
        IFile file;

        ProductList _products;
        public static Product product;
        int count = 0;
        public string _id;
        
        public Order(string id,Home home)
        {
            InitializeComponent();
            relative_page = home;
            LoadCurrentUser();
            LoadProductList();
            LoadProduct(id);
            examinationTextBlock();
        }

        #region LOAD_ALL_DATA

        //загрузка текущего пользователя
        void LoadCurrentUser()
        {
            string path_currentUser = NextLevelPath.CURRENT_USER;
            file = new BinnaryFile(path_currentUser);
            current_user = accounts.getUserByLogin(file.Load<string>());
        }

        //подгружает продукты из бд
        void LoadProductList() => _products = new ProductList();

        //находит нужный продукт
         public void LoadProduct(string id)
        {
            product = _products.getProductById(id);
            if (product == null)
                product = new Product();
            else
            {
                LoadPriceDescription();
                LoadImage();
            }
        }

        void examinationTextBlock()
        {
            string tmp = Count.Text;
            int a = int.Parse(tmp);
            if(a > product.productCount || a < 0)
            {
                Count.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else Count.BorderBrush = new SolidColorBrush(Colors.Black);

            if (string.IsNullOrEmpty(Count.Text))
            {
                Count.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
                Count.BorderBrush = new SolidColorBrush(Colors.Black);
            
        }

        //подгружает описание и цену из бд
        void LoadPriceDescription()
        {
            if (product.descriptionProduct == string.Empty)
                Description.Text = "No description";
            else
                Description.Text = product.descriptionProduct;

            TotalPrice.Text = "Price: " + product.productPrice.ToString();
            countText.Text = "Count: " + product.productCount.ToString();
        }

        //подгружает фото из бд
        void LoadImage()
        {
            string target = NextLevelPath.STOREBD_PATH;
            target = System.IO.Path.GetFullPath(target);
            target = System.IO.Path.Combine(target, product.productName);
            target = System.IO.Path.Combine(target, product.productPhoto);
            if (File.Exists(target))
            {
                var productPhoto = createImageBox();
                productPhoto.Source = loadPhoto(target);
                gridPhoto.Children.Add(productPhoto);
            }
        }

        #endregion

        #region EVENTS

        private void back_Click(object sender, RoutedEventArgs e)
        {
            relative_page.LoadProducts();

        }

        #endregion

        #region CREATE_ELEMENTS

        Image createImageBox()
        {
            Image imageBox = new Image();
            imageBox.Height = 200;
            imageBox.Width = 200;
            return imageBox;
        }

        //загружает фото
        BitmapImage loadPhoto(string path)
        {
            BitmapImage img = new BitmapImage();
            if (File.Exists(path))
            {
                img.BeginInit();
                img.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
                img.DecodePixelWidth = 200;
                img.DecodePixelHeight = 200;
                img.EndInit();
                return img;
            }
            return null;
        }
        #endregion


      

        private void plus_Click_1(object sender, RoutedEventArgs e)
        {
            if(count > product.productCount - 1)
            {
                Count.BorderBrush = new SolidColorBrush(Colors.Red);
            }else
            {
                count++;
                Count.Text = count.ToString();
            }
            double totalprice = count * product.productPrice;
            Price.Text = totalprice.ToString() + " grn";

        }

        private void minus_Click_1(object sender, RoutedEventArgs e)
        {
            if (count > 0)
            {
                count--;
            }
            else if( count > 0)
                Count.BorderBrush = new SolidColorBrush(Colors.Red);

            Count.Text = count.ToString();
            double totalprice = count * product.productPrice;
            Price.Text = totalprice.ToString() + " grn";
        }

        private void Count_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            string tmp = Count.Text;
            int a;
            if (string.IsNullOrEmpty(Count.Text))
            {
                Count.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                Count.BorderBrush = new SolidColorBrush(Colors.Black);
                a = int.Parse(tmp);
            }
                     
            }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProductList cart = new ProductList(NextLevelPath.CART_PATH);
            
            var cartTemporary = cart.getProductByIdAndCustomer(product.Id,current_user.Login);
            if (cartTemporary == null)
            {
                _products.deleteProduct(product);

                product.productCount--;
                product.Liked = true;

                _products.AddNew(product);

                product.currentCount = 1;
                product.customer = current_user.Login;

                cart.AddNew(product);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
