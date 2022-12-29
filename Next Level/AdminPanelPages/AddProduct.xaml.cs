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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Next_Level.AdminPanelPages
{
    public partial class AddProduct : Page
    {
        //исходная ссылка на фото которое загружаешь
        string sourcePhoto = String.Empty;
        //ссылка на базу проекта
        string target = NextLevelPath.STOREBD_PATH;
        //ссылка на файл категорий
        string categoryPath = NextLevelPath.CATEGORIES_PATH;
        //для проверки условия ввода данных
        delegate bool Conditions();

        //список категорий
        List<string> categories = new List<string>();
        //продукты
        ProductList products;
        //интерфейс для записи и выгрузки данных
        IFile file;

        public AddProduct()
        {
            InitializeComponent();
            basicSettings();
            loadCategories();
        }
        #region CONSTRUCTOR_ACTIONS
        //подключение кнопок к событиям, загрузка бд
        private void basicSettings()
        {
            addProduct.Click += new RoutedEventHandler(addProduct_but);
            addPhoto.Click += new RoutedEventHandler(addProductPhoto_but);
            categories = new List<string>();
            products = new ProductList();
            file = null;
        }
        //выгрузка категорий
        private void loadCategories()
        {
            if (File.Exists(categoryPath))
            {
                //MessageBox.Show("Categories is loaded");
                file = new XmlFormat(categoryPath);
                categories = file.Load<List<string>>();
                if (categories == null)
                {
                    categories = new List<string>();
                    createNew.IsChecked = true;
                    createNew.IsEnabled = false;
                }
                else
                {
                    foreach (var category in categories)
                    {
                        comboCategory.Items.Add(category);
                    }
                    comboCategory.SelectedIndex = 0;
                }

            }
        }
       
        #endregion

        #region WORK_WITH_FILE
        //сохранение ктаегории в файл
        private void AddCategory(string categoryName)
        {
            bool IsExists = false;
            if (categories.Count == 0)
            {
                file = new XmlFormat(categoryPath);
                categories.Add(categoryName);
                file.Save(categories);
            }
            else
            {
                foreach (var category in categories)
                {
                    if (category == categoryName)
                    {
                        IsExists = true;
                        break;
                    }
                }
                if (!IsExists)
                {
                    file = new XmlFormat(categoryPath);
                    categories.Add(categoryName);
                    file.Save(categories);
                }
            }
        }

        //Битмап плохо работает с относительными ссылками
        //Для создания ссылки создал этот метод
        //Здесь также копируется в базу картинка которую подгружаешь для продукта
        private string getPhotoPath(string nameProduct)
        {
            if (sourcePhoto != string.Empty)
            {
                string fileExtension = System.IO.Path.GetExtension(sourcePhoto);
                string product_name = nameProduct + fileExtension;
                string photoDir = System.IO.Path.Combine(System.IO.Path.GetFullPath(target), nameProduct);
                if (!Directory.Exists(photoDir))
                    Directory.CreateDirectory(photoDir);
                string targetPhoto = System.IO.Path.Combine(photoDir, product_name);
                File.Copy(sourcePhoto, targetPhoto, true);
                sourcePhoto = string.Empty;
                return product_name;
            }
            return string.Empty;
        }
        #endregion


        //очистка полей после того как нажал добавить продукт
        private void clearFields()
        {
            productName.Text = string.Empty;
            productCategory.Text = string.Empty;

            productPrice.Text = string.Empty;
            productCount.Text = string.Empty;
            productDescription.Text = string.Empty;

            gridPhoto.Children.Clear();
        }



        #region ADD_PRODUCT_CONDITIONS
        //проверяет на содержимость пустоты или пробелов поле имя продукта
        private bool productNameIsEmpty()
        {
            Regex spaces = new Regex("^\\s*$");
            if (productName.Text == string.Empty || spaces.IsMatch(productName.Text))
            {
                //MessageBox.Show("name!!!! ne ok");
                return false;
            }
            else return true;
        }
        //проверяет на содержимость пустоты или пробелов поле категория продукта
        private bool productCategoryIsEmpty()
        {
            Regex spaces = new Regex("^\\s*$");
            if (productCategory.Text == string.Empty || spaces.IsMatch(productCategory.Text))
            {
                if (createNew.IsChecked == true)
                    return false;
                else return true;
                //MessageBox.Show("Cat!!!! ne ok");
            }
            else return true;
        }
        //проверяет на содержимость пустоты или пробелов поле цена продукта
        private bool productPriceIsEmpty()
        {
            Regex spaces = new Regex("^\\s*$");
            if (productPrice.Text == string.Empty || spaces.IsMatch(productPrice.Text))
            {
                //MessageBox.Show("Cena!!!! ne ok");
                return false;
            }
            else return true;
        }
        //проверяет на содержимость пустоты или пробелов поле количество продуктов
        private bool productCountIsEmpty()
        {
            Regex spaces = new Regex("^\\s*$");
            if (productCount.Text == string.Empty || spaces.IsMatch(productCount.Text))
            {
                //MessageBox.Show("count!!! ne ok");
                return false;
            }
            else return true;
        }
        //проверяет на правильность формата поле количество продуктов
        private bool productCountCheckFormat()
        {
            Regex count = new Regex("^\\d+$");
            if (count.IsMatch(productCount.Text))
            {
                //MessageBox.Show("Count ne ok");
                return true;
            }
            else return false;
        }
        //проверяет на правильность формата поле цена продукта
        private bool productPriceCheckFormat()
        {
            Regex price = new Regex("^\\d+,\\d{1,2}$");
            if (price.IsMatch(productPrice.Text))
            {
                //MessageBox.Show("Cena ok");
                return true;
            }
            else return false;
        }
        //проверяются все условия ввода данных
        bool checkAddConditions()
        {
            Conditions conditions = new Conditions(productNameIsEmpty);
            conditions += productCategoryIsEmpty;
            conditions += productPriceIsEmpty;
            conditions += productCountIsEmpty;
            conditions += productCountCheckFormat;
            conditions += productPriceCheckFormat;
            bool IsOk = true;
            foreach (Conditions condition in conditions.GetInvocationList())
            {
                if (condition() == false)
                {
                    IsOk = false;
                    break;
                }
            }
            return IsOk;
        }
        #endregion

        

        #region EVENTS

        //Проверяет формат ввода цены
        private void priceCheck(object sender, TextChangedEventArgs e)
        {
            Regex price = new Regex("^\\d+,\\d{1,2}$");
            if (!price.IsMatch(productPrice.Text))
                productPrice.BorderBrush = Brushes.Red;
            else productPrice.BorderBrush = Brushes.DarkGreen;
            if (productPrice.Text != string.Empty)
                priceBlock.Text = "Price: " + productPrice.Text;
            else
                priceBlock.Text = "Price";
        }
        //Проверяет формат ввода кол-ва продуктов
        private void countCheck(object sender, TextChangedEventArgs e)
        {
            Regex price = new Regex("^\\d+$");
            if (!price.IsMatch(productCount.Text))
                productCount.BorderBrush = Brushes.Red;
            else productCount.BorderBrush = Brushes.DarkGreen;
            if (productCount.Text != string.Empty)
                itemBlock.Text = "Item count: " + productCount.Text;
            else
                itemBlock.Text = "Item count";
        }


        //ДОБАВЛЯЕТ ТОВАР В БАЗУ
        private void addProduct_but(object sender, RoutedEventArgs e)
        {
            //Условия ввода данных выполнились?
            bool IsOk = checkAddConditions();
            if (IsOk)
            {
                //Создаём продукт
                Product product = new Product();
                product.productName = productName.Text;
                if (createNew.IsChecked == true)
                {
                    product.Category = productCategory.Text;
                    //Записываем категорию в файл
                    AddCategory(product.Category);
                }
                else
                {
                    product.Category = comboCategory.SelectedItem.ToString();
                }
                //если пользователь ввёл запятую заменятся на точку
                if (productPrice.Text.Contains(','))
                    productPrice.Text.Replace(',', '.');
                //запись данных в продукт
                product.Id = "nl" + generateId();
                product.productPrice = double.Parse(productPrice.Text);
                product.productCount = int.Parse(productCount.Text);
                product.productPhoto = getPhotoPath(product.productName);
                product.descriptionProduct = productDescription.Text;
                //записывает в файл продукт
                products.AddNew(product);
                //уведомление о том что продукт создан
                ProductView productView = new ProductView(product);
                productView.ShowDialog();
                //очистка полей
                clearFields();
            }
        }
        string generateId()
        {
            string result = string.Empty;
            Random random = new Random();
            int[] id = new int[5];
            while (true)
            {
                result = string.Empty;
                for (int i = 0; i < 5; i++)
                {
                    id[i] = random.Next(0, 9);
                    result += id[i];
                }
                if (products.idIsUnique(result))
                    break;
            }

            return result;
        }

        //Достаёт полный путь к картинке которую загружаем для продкута

        private void addProductPhoto_but(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            if (openFile.ShowDialog() == true)
            {
                sourcePhoto = openFile.FileName;
            }
            gridPhoto.Children.Clear();
            gridPhoto.Children.Add(createImageBox(sourcePhoto));
        }

        #endregion

        #region CREATE_ELEMENTS

        //загрузка фото
        BitmapImage loadPhoto(string path)
        {
            BitmapImage img = new BitmapImage();
            if (File.Exists(path))
            {
                img.BeginInit();
                img.UriSource = new Uri(path);
                img.DecodePixelWidth = 120;
                img.DecodePixelHeight = 120;
                img.EndInit();
                return img;
            }
            return null;
        }

        //создание имаджбокса
        Image createImageBox(string photoPath)
        {
            Image img = new Image();
            img.Source = loadPhoto(photoPath);
            return img;
        }

        //возвращает 16-ричный цвет
        SolidColorBrush SetColor(string hex)
        {
            return (SolidColorBrush)(new BrushConverter().ConvertFrom(hex));
        }

        //Динамическое создание объекта продукт
        //Параметры: продукт, цвет фона, цвет текста


        #endregion

        private void createNew_Checked(object sender, RoutedEventArgs e)
        {
            if(createNew.IsChecked==true)
            {
                comboCategory.Visibility = Visibility.Collapsed;
                productCategory.Visibility = Visibility.Visible;
            }
            
        }

        private void createNew_Unchecked(object sender, RoutedEventArgs e)
        {
            if (createNew.IsChecked == false)
            {
                comboCategory.Visibility = Visibility.Visible;
                productCategory.Visibility = Visibility.Collapsed;
            }
        }

       

        private void comboCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(createNew.IsChecked==false)
                categoryBlock.Text = comboCategory.SelectedItem.ToString();
        }

        private void productName_TextChanged(object sender, TextChangedEventArgs e)
        {
           if (productName.Text != string.Empty)
               productBlock.Text = productName.Text;
           else
               productBlock.Text = "ProductName";
        }


        private void productCategory_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (createNew.IsChecked == true)
            {
                if (productCategory.Text != string.Empty)
                    categoryBlock.Text = productCategory.Text;
                else
                    categoryBlock.Text = "Category";
            }
        }
    }
}
