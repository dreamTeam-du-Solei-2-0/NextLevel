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
using static System.Net.Mime.MediaTypeNames;

namespace Next_Level
{
    /// <summary>
    /// Логика взаимодействия для ProductInfo.xaml
    /// </summary>
    /// 
    public partial class ProductInfo : Window
    {
        List<string> files = new List<string>();
        public string current_user { get; set; }
        int counter = 0;
        Accounts accounts = new Accounts();
        IFile file;
        string path_currentUser = @"..\Debug\CurrentLogin.bin";
        
        public ProductInfo()
        {
            InitializeComponent();
            DirectoryInfo directoryInfo = new DirectoryInfo(@"C:\Users\Alex\Desktop\1");
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.TIF, *.PNG, *.ICO, *.EMF, *.WMF)|*.bmp;*.jpg;*.gif; *.tif; *.png; *.ico; *.emf; *.wmf";
            file = new BinnaryFile(path_currentUser);
            current_user = file.Load<string>();

            //MyImage.Source = new BitmapImage(new Uri(openDialog.FileName));


        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            //BitmapImage b = new BitmapImage();
            //b.UriSource = new Uri("Assets\\Images\\google.png");
            //im.Source = b;
        }

        private void send_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user = accounts.getUserByLogin(this.current_user);
            if(string.IsNullOrEmpty(ComW.Text))
            {
                MessageBox.Show("Is Empty");
               
            }
            else
            {
                Coments.Text += $"\n{user.Name}\n{ComW.Text}";
            
            }
            
            ComW.Clear();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void products_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                User user = new User();
                user = accounts.getUserByLogin(this.current_user);
                if (string.IsNullOrEmpty(ComW.Text))
                {
                    MessageBox.Show("Is Empty");

                }
                else
                {
                    Coments.Text += $"\n{user.Name}\n{ComW.Text}";
                }

                ComW.Clear();
            }
        }



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


    }
}


