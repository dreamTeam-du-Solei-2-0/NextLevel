using Next_Level.Classes;
using Next_Level.Interfaces;
using System;
using System.Collections;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Next_Level.Pages
{
    public partial class Home : Page
    {
        public string current_user { get; set; }
        string path_currentUser = NextLevelPath.CURRENT_USER;
        IFile file;
        ProductList products = null;

        WrapPanel wrap=null;

        List<String> categories;

        public Home()
        {
            InitializeComponent();
            file = new BinnaryFile(path_currentUser);
            this.current_user = file.Load<string>();
            LoadProducts();
            
        }

        public void LoadProducts()
        {
            ScrollViewer scroll = createScroll();
            StackPanel myStack = createStackPanel();
            scroll.Content = myStack;
            homeView.Child = scroll;
            products = new ProductList();

            file = new XmlFormat(NextLevelPath.CATEGORIES_PATH);
            categories = file.Load<List<string>>();
            if (categories == null)
                categories = new List<string>();

            if (products.fileLoad&&products.products.Count!=0)
            {
                if (categories.Count != 0)
                {
                    foreach(var category in categories)
                    {
                        if (products.isHaveCategory(category))
                        {
                            myStack.Children.Add(createCategory(category));
                        }
                        wrap = new WrapPanel();
                        myStack.Children.Add(wrap);
                        foreach (var product in products)
                        {
                            if(product.Category==category)
                                wrap.Children.Add(CreateProduct(product, (SolidColorBrush)FindResource("TertiaryBackgroundColor"), (SolidColorBrush)FindResource("PrimaryTextColor")));
                        }
                    }
                }
                else
                {
                    wrap = new WrapPanel();
                    myStack.Children.Add(wrap);
                    foreach (var product in products)
                    {
                        wrap.Children.Add(CreateProduct(product, (SolidColorBrush)FindResource("TertiaryBackgroundColor"), (SolidColorBrush)FindResource("PrimaryTextColor")));
                    }
                }
            }
        }

        #region EVENTS
        private void button_BuyProduct(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Frame myFrame = createFrame();
            myFrame.Navigate(new Order(button.Name.Remove(button.Name.Length - 1),this));
            homeView.Child = myFrame;
        }

        private void button_InfoProduct(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Frame myFrame = createFrame();
            myFrame.Navigate(new ProductInfo(button.Name.Remove(button.Name.Length-1)));
            homeView.Child = myFrame;
        }
        


        #endregion

        #region CREATE COLORS
        SolidColorBrush makeRandomColor()
        {
            Random random = new Random();
            int red = random.Next(0, 255);
            int green = random.Next(0, 255);
            int blue = random.Next(0, 255);
            var color = Color.FromRgb(Convert.ToByte(red), Convert.ToByte(green), Convert.ToByte(blue));
            return new SolidColorBrush(color);
        }
        SolidColorBrush makeColorRgb(int red, int green, int blue)
        {
            var color = Color.FromRgb(Convert.ToByte(red), Convert.ToByte(green), Convert.ToByte(blue));
            return new SolidColorBrush(color);
        }
        SolidColorBrush SetColor(string hex)
        {
            return (SolidColorBrush)(new BrushConverter().ConvertFrom(hex));
        }
        #endregion

        #region CREATE ELEMENTS

        Frame createFrame()
        {
            Frame frame = new Frame();
            frame.Background = (SolidColorBrush)FindResource("PrimaryBackgroundColor");
            return frame;
        }

        StackPanel createStackPanel()
        {
            StackPanel stackPanel = new StackPanel();
            return stackPanel;
        }

        ScrollViewer createScroll()
        {
            ScrollViewer scrollViewer = new ScrollViewer();
            scrollViewer.Background = (SolidColorBrush)FindResource("SecundaryBackgroundColor");
            scrollViewer.Margin = new Thickness(10, 0, 10, 0);
            return scrollViewer;
        }
        TextBlock createCategory(string _category)
        {
            TextBlock category = new TextBlock();
            category.Text = _category;
            category.FontSize = 20;
            category.FontWeight = FontWeights.Bold;
            category.Foreground = (SolidColorBrush)FindResource("PrimaryTextColor");
            category.HorizontalAlignment = HorizontalAlignment.Center;
            category.Margin = new Thickness(10);
            return category;
        }

        BitmapImage loadPhoto(string path)
        {
            BitmapImage img = new BitmapImage();
            if (File.Exists(path))
            {
                img.BeginInit();
                img.UriSource = new Uri(path,UriKind.RelativeOrAbsolute);
                img.DecodePixelWidth = 120;
                img.DecodePixelHeight = 120;
                img.EndInit();
                return img;
            }
            return null;
        }

        Border CreateProduct(Product product, SolidColorBrush gridColor, SolidColorBrush textColor)
        {
            int ROWS_COUNT = 6;
            int COLUMNS_COUNT = 2;

            //Создание рамки. Для скругления углов
            Border border = new Border();
            border.CornerRadius = new CornerRadius(8);
            border.Background = gridColor;
            border.Height = 265;
            border.Width = 180;
            border.Margin = new Thickness(8);

            //Эффект тени для рамки
            DropShadowEffect shadowEffect = new DropShadowEffect();
            shadowEffect.BlurRadius = 8;
            shadowEffect.Opacity = 0.5;
            border.Effect = shadowEffect;

            //Создание сетки
            Grid myGrid = new Grid();
            myGrid.Height = 265;
            myGrid.Width = 180;
            //показать линии сетки
            //myGrid.ShowGridLines = true;

            //Создание строк для сетки
            RowDefinition[] rows = new RowDefinition[ROWS_COUNT];
            for (int i = 0; i < rows.Length; i++)
                rows[i] = new RowDefinition();

            //Создание столбцов для сетки
            ColumnDefinition[] columns = new ColumnDefinition[COLUMNS_COUNT];
            for (int i = 0; i < columns.Length; i++)
                columns[i] = new ColumnDefinition();

            //добавялем столбцы в сетку
            myGrid.ColumnDefinitions.Add(columns[0]);
            myGrid.ColumnDefinitions.Add(columns[1]);

            //меняем высоту первой строки
            rows[1].Height = new GridLength(120);
            rows[5].Height = new GridLength(50);
            //добавляем строки в сетку
            myGrid.RowDefinitions.Add(rows[0]);
            myGrid.RowDefinitions.Add(rows[1]);
            myGrid.RowDefinitions.Add(rows[2]);
            myGrid.RowDefinitions.Add(rows[3]);
            myGrid.RowDefinitions.Add(rows[4]);
            myGrid.RowDefinitions.Add(rows[5]);

            //Фото товара
            Grid photo = new Grid();
            photo.Height = 120;
            photo.Width = 120;

            //Категория
            Border categoryBorder = new Border();
            categoryBorder.Background = (SolidColorBrush)FindResource("PrimaryBackgroundColor");
            categoryBorder.HorizontalAlignment = HorizontalAlignment.Center;
            categoryBorder.CornerRadius = new CornerRadius(8);
            categoryBorder.Margin = new Thickness(2);

            TextBlock category = new TextBlock();
            if (product.Category != String.Empty)
                category.Text = product.Category;
            else
                category.Text = "#CATEGORY#";

            category.Margin = new Thickness(3);
            category.FontSize = 12;
            category.TextWrapping = TextWrapping.Wrap;
            category.VerticalAlignment = VerticalAlignment.Center;
            category.TextAlignment = TextAlignment.Center;
            category.Foreground = textColor;

            categoryBorder.Child = category;
            //Добавляю в строку
            Grid.SetRow(categoryBorder, 0);
            //Растягиваю на два столбца
            Grid.SetColumnSpan(categoryBorder, 2);
            //Добавляю текст в сетку
            myGrid.Children.Add(categoryBorder);

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


            Grid.SetRow(photo, 1);
            Grid.SetColumnSpan(photo, 2);
            myGrid.Children.Add(photo);

            //Items count
            TextBlock itemsCount = new TextBlock();
            itemsCount.Text = $"Items count: {product.productCount}";
            itemsCount.FontSize = 12;
            itemsCount.TextWrapping = TextWrapping.Wrap;
            itemsCount.VerticalAlignment = VerticalAlignment.Center;
            itemsCount.TextAlignment = TextAlignment.Center;
            itemsCount.Foreground = textColor;
            //Добавляю в строку
            Grid.SetRow(itemsCount, 2);
            //Растягиваю на два столбца
            Grid.SetColumnSpan(itemsCount, 2);
            //Добавляю текст в сетку
            myGrid.Children.Add(itemsCount);

            //Название товара
            TextBlock productName = new TextBlock();
            if (product.productName != String.Empty)
                productName.Text = product.productName;
            else productName.Text = "#PRODUCT_NAME#";
            productName.FontSize = 15;
            productName.TextWrapping = TextWrapping.Wrap;
            productName.VerticalAlignment = VerticalAlignment.Center;
            productName.TextAlignment = TextAlignment.Center;
            productName.Foreground = textColor;
            //Добавляю в строку
            Grid.SetRow(productName, 3);
            //Растягиваю на два столбца
            Grid.SetColumnSpan(productName, 2);
            //Добавляю текст в сетку
            myGrid.Children.Add(productName);

            //Цена товара
            TextBlock price = new TextBlock();
            price.Text = $"{product.productPrice} grn";
            price.TextAlignment = TextAlignment.Center;
            price.VerticalAlignment = VerticalAlignment.Center;
            price.FontSize = 15;
            price.Foreground = textColor;
            Grid.SetRow(price, 4);
            Grid.SetColumnSpan(price, 2);
            myGrid.Children.Add(price);

            Border buyBorder = new Border();
            buyBorder.Background = SetColor("#15531C");
            buyBorder.CornerRadius = new CornerRadius(8);
            buyBorder.BorderThickness = new Thickness(1);
            buyBorder.Margin = new Thickness(10);

            //Кнопка купить
            Button buyBut = new Button();
            buyBut.BorderThickness = new Thickness(0);
            if (product.Id != string.Empty)
                buyBut.Name = product.Id + "1";
            buyBut.Content = "Buy";
            buyBut.Foreground = Brushes.White;
            buyBut.Background = SetColor("#15531C");
            buyBut.Margin = new Thickness(2);
            buyBut.Foreground = Brushes.White;
            buyBut.Click += new RoutedEventHandler(button_BuyProduct);
            buyBorder.Child = buyBut;
            Grid.SetRow(buyBorder, 5);
            myGrid.Children.Add(buyBorder);

            Border infoBorder = new Border();
            infoBorder.Background = SetColor("#d32f2f");
            infoBorder.CornerRadius = new CornerRadius(8);
            infoBorder.BorderThickness = new Thickness(1);
            infoBorder.Margin = new Thickness(10);

            //Кнопка информация о товаре
            Button infoBut = new Button();
            infoBut.BorderThickness = new Thickness(0);
            if (product.Id != string.Empty)
                infoBut.Name = product.Id + "2";
            infoBut.Content = "About";
            infoBut.Foreground = Brushes.White;
            infoBut.Background = SetColor("#d32f2f");
            infoBut.Margin = new Thickness(2);
            infoBut.Foreground = Brushes.White;
            infoBut.Click += new RoutedEventHandler(button_InfoProduct);
            infoBorder.Child = infoBut;

            if (product.productCount == 0)
            {
                itemsCount.Text = "Product is out";
                itemsCount.Foreground = Brushes.DarkRed;
                buyBorder.Visibility = Visibility.Collapsed;
                Grid.SetRow(infoBorder, 5);
                Grid.SetColumnSpan(infoBorder, 2);
            }
            else
            {
                Grid.SetRow(infoBorder, 5);
                Grid.SetColumn(infoBorder, 1);
            }

            myGrid.Children.Add(infoBorder);

            

            //добавляю в рамку сетку
            border.Child = myGrid;
            return border;
        }
        #endregion

    }

}