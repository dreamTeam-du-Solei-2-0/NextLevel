using Microsoft.Win32;
using Next_Level.Classes;
using Next_Level.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
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
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Next_Level
{
    
    public partial class ProductInfo : Window
    {
        List<string> files = new List<string>();
        public string current_user { get; set; }
        int counter = 0;
        Accounts accounts = new Accounts();
        IFile file;
        string path_currentUser = NextLevelPath.CURRENT_USER;
        ProductList _products;
        Product product;
        List<Feedback> feedbacks = new List<Feedback>();
        Product p = new Product();
        

        public ProductInfo(string id)
        {
            InitializeComponent();
            file = new BinnaryFile(path_currentUser);
            current_user = file.Load<string>();
            MaxWidth = 600;
            _products = new ProductList();
            product = _products.getProductById(id);
            //if (product.descriptionProduct != string.Empty)
            ////    product.descriptionProduct.Content. = product.descriptionProduct.ToString();
            LoadComments();
            Des.Text += "\nОчень классный клоун";
        }

        #region FEEDBACKS        
        private void send_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user = accounts.getUserByLogin(this.current_user);
            if(string.IsNullOrEmpty(ComW.Text))
            {
                //MessageBox.Show("Is Empty");
               
            }
            else
            {
                Feedback feedback= new Feedback();
                feedback.username=user.Name;
                feedback.comment = ComW.Text;
                feedback.date = $"{DateTime.Today.Year}-{DateTime.Today.Month}-{DateTime.Today.Day}  {DateTime.Now.Hour}:{DateTime.Now.Minute}";
                Coments.Children.Add(CreateGrid(feedback.username,feedback.comment, feedback.date));
                SaveComments(feedback);
            }
            
            ComW.Clear();

        
        }

        private void products_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                User user = new User();
                user = accounts.getUserByLogin(this.current_user);
                if (string.IsNullOrEmpty(ComW.Text))
                {
                    //MessageBox.Show("Is Empty");
                }

                else
                {
                    Feedback feedback = new Feedback();
                    feedback.username = user.Name;
                    feedback.comment = ComW.Text;
                    feedback.date = $"{DateTime.Today.Year}-{DateTime.Today.Month}-{DateTime.Today.Day}  {DateTime.Now.Hour}:{DateTime.Now.Minute}";
                    Coments.Children.Add(CreateGrid(feedback.username, feedback.comment, feedback.date));
                    SaveComments(feedback);
                }

                ComW.Clear();
            }
        }

        void LoadComments()
        {
            string target = NextLevelPath.STOREBD_PATH;
            target = System.IO.Path.GetFullPath(target);
            target = System.IO.Path.Combine(target, product.productName);
            target = System.IO.Path.Combine(target, product.productName + ".xml");
            if (File.Exists(target))
            {
                file = new XmlFormat(target);
                feedbacks = file.Load<List<Feedback>>();
                foreach(var feedback in feedbacks)
                {
                    Coments.Children.Add(CreateGrid(feedback.username,feedback.comment,feedback.date));
                }
            }
            else feedbacks = new List<Feedback>();


        }
        //Сохраняет комментарий
        void SaveComments(Feedback feedback)
        {

            feedbacks.Add(feedback);
            string target = NextLevelPath.STOREBD_PATH;
            target = System.IO.Path.GetFullPath(target);
            target = System.IO.Path.Combine(target, product.productName);
            target = System.IO.Path.Combine(target, product.productName + ".xml");
            file = new XmlFormat(target);
            file.Save(feedbacks);
        }
        #endregion

        #region CREATE_ELEMENTS
        //Установка 16-ричного цвета
        Brush SetColor(string hex)
        {
            return (Brush)(new BrushConverter().ConvertFrom(hex));
        }
        
        //Создаёт комментарий
        Grid CreateGrid(string userName, string commentText,string currentdate)
        {
            //тело отзыва
            Grid myGrid = new Grid();
            myGrid.MaxHeight = 350;
            myGrid.MinHeight = 150;
            myGrid.Background = SetColor("#1F1F1F");
            myGrid.Margin = new Thickness(5);
            //myGrid.ShowGridLines = true;

            RowDefinition[] rows = new RowDefinition[3];
            for (int i = 0; i < rows.Length; i++)
                rows[i] = new RowDefinition();

            ColumnDefinition[] columns = new ColumnDefinition[4];
            for (int i = 0; i < columns.Length; i++)
                columns[i] = new ColumnDefinition();

            rows[0].Height = new GridLength(25);
            rows[1].Height = new GridLength(100);
            rows[2].Height = new GridLength(25);

            columns[0].Width = new GridLength(110);
            columns[1].Width = new GridLength(100);
            columns[3].Width = new GridLength(120);

            myGrid.ColumnDefinitions.Add(columns[0]);
            myGrid.ColumnDefinitions.Add(columns[1]);
            myGrid.ColumnDefinitions.Add(columns[2]);
            myGrid.ColumnDefinitions.Add(columns[3]);

            myGrid.RowDefinitions.Add(rows[0]);
            myGrid.RowDefinitions.Add(rows[1]);
            myGrid.RowDefinitions.Add(rows[2]);

            //кнопка ответить
            Button answer = new Button();
            answer.Content = "Reply";
            answer.Foreground = SetColor("#00541F");

            //имя пользователя
            TextBlock textName = new TextBlock();
            textName.Text = userName;
            textName.Foreground = SetColor("#B4B4B4");
            textName.Margin = new Thickness(10, 0, 0, 0);
            textName.FontSize = 20;

            //текст отзыва
            TextBlock feed = new TextBlock();
            feed.Margin = new Thickness(10);
            feed.Text = commentText;
            feed.Foreground = SetColor("#B4B4B4");
            feed.TextWrapping = TextWrapping.Wrap;
            feed.Margin = new Thickness(10, 0, 0, 0);
            feed.FontSize = 15;

            //время
            TextBlock currentDate = new TextBlock();
            currentDate.Margin = new Thickness(10);
            currentDate.Text = currentdate;
            currentDate.Foreground = SetColor("#B4B4B4");
            currentDate.TextWrapping = TextWrapping.Wrap;
            currentDate.Margin = new Thickness(10, 0, 0, 0);
            currentDate.FontSize = 12;
            currentDate.VerticalAlignment = VerticalAlignment.Center;

           ///ДОБАВЛЕНИЕ В ГРИД

            //фото
            Grid grid1 = new Grid();
            grid1.Margin = new Thickness(5);
            grid1.Background = SetColor("#B4B4B4");
            Grid.SetRowSpan(grid1, 2);
            myGrid.Children.Add(grid1);

            //имя пользователя
            Grid.SetRow(textName, 0);
            Grid.SetColumn(textName, 1);
            Grid.SetColumnSpan(textName, 2);
            myGrid.Children.Add(textName);

            //отзыв
            Grid.SetRow(feed, 1);
            Grid.SetColumn(feed, 1);
            Grid.SetColumnSpan(feed, 3);
            myGrid.Children.Add(feed);


            //текущая дата
            Grid.SetColumn(currentDate, 3);
            myGrid.Children.Add(currentDate);
            sc.MinHeight = 150;
            sc.MaxHeight = 1000;

            return myGrid;
        }

        #endregion

        #region GALLERY_EVENTS

        //private void button4_Click(object sender, RoutedEventArgs e)
        //{
        //    //BitmapImage b = new BitmapImage();
        //    //b.UriSource = new Uri("Assets\\Images\\google.png");
        //    //im.Source = b;
        //}
        //private void First_Click(object sender, RoutedEventArgs e)
        //{
        //    MyImage.Source = new BitmapImage(new Uri(files[0]));
        //    counter = 0;

        //}

        //private void Prev_Click(object sender, RoutedEventArgs e)
        //{
        //    if (counter - 1 >= 0)
        //        counter--;
        //    MyImage.Source = new BitmapImage(new Uri(files[counter]));

        //}

        //private void Next_Click(object sender, RoutedEventArgs e)
        //{
        //    if (counter + 1 < files.Count)
        //        counter++;
        //    MyImage.Source = new BitmapImage(new Uri(files[counter]));

        //}

        //private void Last_Click(object sender, RoutedEventArgs e)
        //{
        //    counter = files.Count - 1;
        //    MyImage.Source = new BitmapImage(new Uri(files[counter]));

        //}
        #endregion

    }
}


