using Next_Level.Classes;
using Next_Level.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Next_Level.Pages
{
    public partial class Cart : Page
    {
        IFile file;
        User current_user;
        ProductList allProducts;
        ProductList cartProducts;
        List<CheckBox> cartElements;
        public Cart()
        {
            InitializeComponent();
            file = null;
            LoadCurrentUser();
            cartElements = new List<CheckBox>();
            allProducts = new ProductList();
            cartProducts = new ProductList(NextLevelPath.CART_PATH);
            showProduct();
        }

        void LoadCurrentUser()
        {
            Accounts accounts = new Accounts();
            string path_currentUser = NextLevelPath.CURRENT_USER;
            file = new BinnaryFile(path_currentUser);
            current_user = accounts.getUserByLogin(file.Load<string>());
        }

        void showProduct()
        {
            var product = cartProducts.getProductsByCustomer(current_user.Login);
            if (product!=null)
            {
                productPanel.Children.Clear();
                foreach (var prod in product)
                {
                    productPanel.Children.Add(createCartElement(prod, (SolidColorBrush)FindResource("TertiaryBackgroundColor"), (SolidColorBrush)FindResource("PrimaryTextColor")));
                }
            }
            else
            {
                TextBlock cartInfo = new TextBlock();
                cartInfo.Text = "Cart is epmpty";
                cartInfo.Foreground = (SolidColorBrush)FindResource("PrimaryTextColor");
                cartInfo.VerticalAlignment = VerticalAlignment.Center;
                cartInfo.HorizontalAlignment = HorizontalAlignment.Center;
                cartInfo.TextWrapping = TextWrapping.Wrap;
                cartInfo.FontSize = 50;
                Grid.SetRow(cartInfo, 1);
                cartBody.Children.Remove(scroll);
                cartBody.Children.Add(cartInfo);
            }
        }


        #region EVENTS
        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if (cartElements.Count != 0)
            {
                foreach (var element in cartElements)
                {
                    if (element.IsChecked == true)
                    {
                        var cartProduct = cartProducts.getProductByIdAndCustomer(element.Name, current_user.Login);
                        var countBox = FindName(element.Name + "Count") as TextBlock;
                        var product = allProducts.getProductById(element.Name);
                        allProducts.deleteProduct(product);
                        product.productCount += int.Parse(countBox.Text);
                        allProducts.AddNew(product);
                        cartProducts.removeProduct(cartProduct);
                        UnregisterName(countBox.Name);
                    }
                }
                showProduct();
            }
        }

        private void unselect_Click(object sender, RoutedEventArgs e)
        {
            if (cartElements.Count != 0)
            {
                foreach (var element in cartElements)
                    element.IsChecked = false;
            }
        }

        private void selectAll_Click(object sender, RoutedEventArgs e)
        {
            if (cartElements.Count != 0)
            {
                foreach (var element in cartElements)
                    element.IsChecked = true;
            }
        }

        private void plusButton(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            string id = button.Name;
            string substr = "plus";
            int position = id.IndexOf(substr);
            id=id.Remove(position, substr.Length);

            var product = allProducts.getProductById(id);
            int productCount=product.productCount;
            productCount--;
            if (productCount >= 0)
            {
                var cartProduct = cartProducts.getProductByIdAndCustomer(id, current_user.Login);
                var countBox = FindName(id + "Count") as TextBlock;
                var priceBox = FindName(id + "Price") as TextBlock;

                int cartCount = int.Parse(countBox.Text);
                ++cartCount;

                allProducts.deleteProduct(product);
                product.productCount--;
                allProducts.AddNew(product);

                cartProducts.deleteProduct(cartProduct);
                cartProduct.currentCount = cartCount;
                cartProduct.productPrice = cartCount * product.productPrice;
                cartProducts.AddNew(cartProduct);

                countBox.Text = cartCount.ToString();
                priceBox.Text = cartProduct.productPrice.ToString();
            }
        }

        private void minusButton(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            string id = button.Name;
            string substr = "minus";
            int position = id.IndexOf(substr);
            id = id.Remove(position, substr.Length);

            var product = allProducts.getProductById(id);
            var cartProduct = cartProducts.getProductByIdAndCustomer(id, current_user.Login);
            var countBox = FindName(id + "Count") as TextBlock;
            var priceBox = FindName(id + "Price") as TextBlock;

            int cartCount = int.Parse(countBox.Text);
            --cartCount;
            if (cartCount != 0)
            {
                allProducts.deleteProduct(product);
                product.productCount ++;
                allProducts.AddNew(product);

                cartProducts.deleteProduct(cartProduct);
                cartProduct.currentCount = cartCount;
                cartProduct.productPrice=product.productPrice*cartCount;
                cartProducts.AddNew(cartProduct);

                countBox.Text = cartCount.ToString();
                priceBox.Text = cartProduct.productPrice.ToString();
            }
        }

        #endregion

        #region CREATE_ELEMENTS
        SolidColorBrush SetColor(string hex)
        {
            return (SolidColorBrush)(new BrushConverter().ConvertFrom(hex));
        }
        BitmapImage loadPhoto(string path)
        {
            BitmapImage img = new BitmapImage();
            if (File.Exists(path))
            {
                img.BeginInit();
                img.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
                img.DecodePixelWidth = 120;
                img.DecodePixelHeight = 120;
                img.EndInit();
                return img;
            }
            return null;
        }

        Border createCartElement(Product product, SolidColorBrush gridColor, SolidColorBrush textColor)
        {
            //main_body

            object element;

            Border border = new Border();
            border.CornerRadius = new CornerRadius(8);
            border.Height = 150;
            border.Width = 800;
            border.Margin = new Thickness(5);
            border.Background = gridColor;

            //body_shadow
            DropShadowEffect shadowEffect = new DropShadowEffect();
            shadowEffect.BlurRadius = 8;
            shadowEffect.ShadowDepth = 8;
            shadowEffect.Opacity = 0.5;

            border.Effect = shadowEffect;

            //main_grid
            Grid grid = new Grid();
            grid.Width = border.Width-10;
            grid.Margin = new Thickness(10);

            ColumnDefinition[] columnDefinitions = new ColumnDefinition[5];

            for (int i = 0; i < columnDefinitions.Length; i++)
            {
                columnDefinitions[i] = new ColumnDefinition();
                grid.ColumnDefinitions.Add(columnDefinitions[i]);
            }

            columnDefinitions[0].Width = new GridLength(0.3, GridUnitType.Star);

            //delete_check
            CheckBox checkBox = new CheckBox();
            checkBox.VerticalAlignment = VerticalAlignment.Center;
            checkBox.HorizontalAlignment = HorizontalAlignment.Center;
            checkBox.Name = product.Id;

            cartElements.Add(checkBox);

            Grid.SetColumn(checkBox, 0);
            grid.Children.Add(checkBox);

            //product_photo
            Grid photo = new Grid();
            photo.Width = 120;
            photo.Height = 120;
            photo.Background = SetColor("#1F1F1F");

            string photoBD = System.IO.Path.GetFullPath(NextLevelPath.STOREBD_PATH);
            photoBD = System.IO.Path.Combine(photoBD, product.productName);
            //Загрузка фото
            var productPhoto = loadPhoto(System.IO.Path.Combine(photoBD, product.productPhoto));
            if (productPhoto != null)
            {
                Image imageBox = new Image();
                imageBox.Source = productPhoto;
                photo.Children.Add(imageBox);
            }
            else
            {
                TextBlock photoInfo = new TextBlock();
                photoInfo.Text = "#PHOTO#";
                photo.Background = SetColor("#1F1F1F");
                photo.Children.Add(photoInfo);
            }

            Grid.SetColumn(photo, 1);
            grid.Children.Add(photo);

            //product_name
            TextBlock productName = new TextBlock();
            productName.Text = product.productName;
            productName.Foreground = textColor;
            productName.VerticalAlignment = VerticalAlignment.Center;
            productName.HorizontalAlignment = HorizontalAlignment.Center;
            productName.TextWrapping = TextWrapping.Wrap;
            productName.FontSize = 25;

            Grid.SetColumn(productName, 2);
            grid.Children.Add(productName);

            //for_plus_countProducts_minus
            WrapPanel wp = new WrapPanel();
            wp.HorizontalAlignment = HorizontalAlignment.Center;
            wp.VerticalAlignment = VerticalAlignment.Center;

            Border buttonBorder = new Border();
            buttonBorder.Background = SetColor("#d32f2f");
            buttonBorder.CornerRadius = new CornerRadius(8);
            buttonBorder.BorderThickness = new Thickness(1);
            buttonBorder.Margin = new Thickness(10);

            Button minus = new Button();
            minus.Background = SetColor("#d32f2f");
            minus.Name = product.Id + "minus";
            minus.Click += new RoutedEventHandler(minusButton);
            minus.Foreground = Brushes.White;
            minus.BorderThickness = new Thickness(0);
            minus.Height = 30;
            minus.Width = 30;
            minus.FontSize = 20;
            minus.Margin = new Thickness(2);
            minus.Content = "-";
            buttonBorder.Child = minus;

            wp.Children.Add(buttonBorder);

            //countProducts
            TextBlock textBlock1 = new TextBlock();
            textBlock1.Text = product.currentCount.ToString();
            textBlock1.Foreground = textColor;
            textBlock1.VerticalAlignment = VerticalAlignment.Center;
            textBlock1.HorizontalAlignment = HorizontalAlignment.Center;
            textBlock1.FontSize = 20;
            textBlock1.Margin = new Thickness(5);

            textBlock1.Name = product.Id + "Count";
            element = FindName(textBlock1.Name) as TextBlock;
            if (element == null)
            {
                RegisterName(textBlock1.Name, textBlock1);
            }

            wp.Children.Add(textBlock1);

            buttonBorder = new Border();
            buttonBorder.Background = SetColor("#15531C");
            buttonBorder.CornerRadius = new CornerRadius(8);
            buttonBorder.BorderThickness = new Thickness(1);
            buttonBorder.Margin = new Thickness(10);

            Button plus = new Button();
            plus.Background = SetColor("#15531C");
            plus.Name = product.Id + "plus";
            plus.Click += new RoutedEventHandler(plusButton);
            plus.Foreground = Brushes.White;
            plus.BorderThickness = new Thickness(0);
            plus.Height = 30;
            plus.Width = 30;
            plus.FontSize = 20;
            plus.Margin = new Thickness(2);
            plus.Content = "+";

            buttonBorder.Child = plus;

            wp.Children.Add(buttonBorder);

            Grid.SetColumn(wp, 3);
            grid.Children.Add(wp);

            StackPanel sp = new StackPanel();

            //product_price
            TextBlock textBlock2 = new TextBlock();
            textBlock2.Foreground = textColor;
            textBlock2.Text = product.productPrice.ToString();
            textBlock2.TextAlignment = TextAlignment.Center;
            textBlock2.TextWrapping = TextWrapping.Wrap;
            textBlock2.FontSize = 25;
            textBlock2.Margin = new Thickness(0, 20, 0, 20);

            textBlock2.Name = product.Id + "Price";
            element = FindName(textBlock2.Name) as TextBlock;
            if (element == null)
            {
                RegisterName(textBlock2.Name, textBlock2);
            }
          

            sp.Children.Add(textBlock2);

            buttonBorder = new Border();
            buttonBorder.Background = SetColor("#15531C");
            buttonBorder.CornerRadius = new CornerRadius(8);
            buttonBorder.BorderThickness = new Thickness(1);
            buttonBorder.Margin = new Thickness(10);

            Button button = new Button();
            button.Background = SetColor("#15531C");
            button.Foreground = Brushes.White;
            button.FontSize = 25;
            button.Margin = new Thickness(2);
            button.Content = "Buy";
            button.BorderThickness = new Thickness(0);

            buttonBorder.Child = button;

            sp.Children.Add(buttonBorder);

            Grid.SetColumn(sp, 4);
            grid.Children.Add(sp);

            border.Child = grid;

            return border;
        }
        #endregion

    }
}
