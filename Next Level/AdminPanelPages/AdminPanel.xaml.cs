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

        //#region CONSTRUCTOR_ACTIONS
        ////подключение кнопок к событиям, загрузка бд
        //private void basicSettings()
        //{
        //    addProduct.Click += new RoutedEventHandler(addProduct_but);
        //    uploadPhoto.Click += new RoutedEventHandler(addProductPhoto_but);
        //    categories = new List<string>();
        //    products = new ProductList();
        //    file = null;
        //}
        ////выгрузка категорий
        //private void loadCategories()
        //{
        //    if (File.Exists(categoryPath))
        //    {
        //        //MessageBox.Show("Categories is loaded");
        //        file = new XmlFormat(categoryPath);
        //        categories = file.Load<List<string>>();
        //    }
        //}
        ////выгрузка продуктов
        //private void loadProducts()
        //{
        //    if (products.fileLoad)
        //    {
        //        //MessageBox.Show("Products is loaded");
        //        WrapPanel wrap;
        //        if (categories.Count != 0)
        //        {

        //            foreach (var category in categories)
        //            {
        //                myStack2.Children.Add(createTextBlock(category));
        //                wrap = new WrapPanel();
        //                myStack2.Children.Add(wrap);
        //                foreach (var product in products)
        //                {
        //                    if (product.Category == category)
        //                        wrap.Children.Add(CreateProduct(product, SetColor("#C0C9EA"), Brushes.Black));
        //                }

        //            }
        //        }
        //        else
        //        {
        //            wrap = new WrapPanel();
        //            myStack2.Children.Add(wrap);
        //            foreach (var product in products)
        //            {
        //                wrap.Children.Add(CreateProduct(product, SetColor("#C0C9EA"), Brushes.Black));
        //            }
        //        }
        //    }
        //}
        //#endregion

        //#region WORK_WITH_FILE
        ////сохранение ктаегории в файл
        //private void AddCategory(string categoryName)
        //{
        //    bool IsExists = false;
        //    if (categories.Count == 0)
        //    {
        //        file = new XmlFormat(categoryPath);
        //        categories.Add(categoryName);
        //        file.Save(categories);
        //    }
        //    else
        //    {
        //        foreach (var category in categories)
        //        {
        //            if (category == categoryName)
        //            {
        //                IsExists = true;
        //                break;
        //            }
        //        }
        //        if (!IsExists)
        //        {
        //            file = new XmlFormat(categoryPath);
        //            categories.Add(categoryName);
        //            file.Save(categories);
        //        }
        //    }
        //}

        ////Битмап плохо работает с относительными ссылками
        ////Для создания ссылки создал этот метод
        ////Здесь также копируется в базу картинка которую подгружаешь для продукта
        //private string getPhotoPath(string nameProduct)
        //{
        //    if (sourcePhoto != string.Empty)
        //    {
        //        string fileExtension = System.IO.Path.GetExtension(sourcePhoto);
        //        string product_name = nameProduct + fileExtension;
        //        string photoDir = System.IO.Path.Combine(System.IO.Path.GetFullPath(target), nameProduct);
        //        if (!Directory.Exists(photoDir))
        //            Directory.CreateDirectory(photoDir);
        //        string targetPhoto = System.IO.Path.Combine(photoDir, product_name);
        //        File.Copy(sourcePhoto, targetPhoto, true);
        //        sourcePhoto = string.Empty;
        //        return product_name;
        //    }
        //    return string.Empty;
        //}
        //#endregion


        ////очистка полей после того как нажал добавить продукт
        //private void clearFields()
        //{
        //    productName.Text = string.Empty;
        //    productCategory.Text = string.Empty;

        //    productPrice.Text = string.Empty;
        //    productCount.Text = string.Empty;
        //    productDescription.Text = string.Empty;

        //    productPhoto.Children.Clear();
        //    TextBlock text = new TextBlock();
        //    text.Text = "Product photo";
        //    text.HorizontalAlignment = HorizontalAlignment.Center;
        //    text.VerticalAlignment = VerticalAlignment.Center;

        //    productPhoto.Children.Add(text);
        //}
        ////очистка полей после того как нажал изменить продукт
        //private void clearEditFields()
        //{
        //    editName.Text = string.Empty;
        //    editCategory.Text = string.Empty;

        //    editPrice.Text = string.Empty;
        //    editCount.Text = string.Empty;
        //    editDescription.Text = string.Empty;

        //    gridPhoto.Children.Clear();
        //    TextBlock text = new TextBlock();
        //    text.Text = "Product photo";
        //    text.HorizontalAlignment = HorizontalAlignment.Center;
        //    text.VerticalAlignment = VerticalAlignment.Center;

        //    productPhoto.Children.Add(text);
        //}



        //#region ADD_PRODUCT_CONDITIONS
        ////проверяет на содержимость пустоты или пробелов поле имя продукта
        //private bool productNameIsEmpty()
        //{
        //    Regex spaces = new Regex("^\\s*$");
        //    if (productName.Text == string.Empty || spaces.IsMatch(productName.Text))
        //    {
        //        //MessageBox.Show("name!!!! ne ok");
        //        return false;
        //    }
        //    else return true;
        //}
        ////проверяет на содержимость пустоты или пробелов поле категория продукта
        //private bool productCategoryIsEmpty()
        //{
        //    Regex spaces = new Regex("^\\s*$");
        //    if (productCategory.Text == string.Empty || spaces.IsMatch(productCategory.Text))
        //    {
        //        //MessageBox.Show("Cat!!!! ne ok");
        //        return false;
        //    }
        //    else return true;
        //}
        ////проверяет на содержимость пустоты или пробелов поле цена продукта
        //private bool productPriceIsEmpty()
        //{
        //    Regex spaces = new Regex("^\\s*$");
        //    if (productPrice.Text == string.Empty || spaces.IsMatch(productPrice.Text))
        //    {
        //        //MessageBox.Show("Cena!!!! ne ok");
        //        return false;
        //    }
        //    else return true;
        //}
        ////проверяет на содержимость пустоты или пробелов поле количество продуктов
        //private bool productCountIsEmpty()
        //{
        //    Regex spaces = new Regex("^\\s*$");
        //    if (productCount.Text == string.Empty || spaces.IsMatch(productCount.Text))
        //    {
        //        //MessageBox.Show("count!!! ne ok");
        //        return false;
        //    }
        //    else return true;
        //}
        ////проверяет на правильность формата поле количество продуктов
        //private bool productCountCheckFormat()
        //{
        //    Regex count = new Regex("^\\d+$");
        //    if (count.IsMatch(productCount.Text))
        //    {
        //        //MessageBox.Show("Count ne ok");
        //        return true;
        //    }
        //    else return false;
        //}
        ////проверяет на правильность формата поле цена продукта
        //private bool productPriceCheckFormat()
        //{
        //    Regex price = new Regex("^\\d+,\\d{1,2}$");
        //    if (price.IsMatch(productPrice.Text))
        //    {
        //        //MessageBox.Show("Cena ok");
        //        return true;
        //    }
        //    else return false;
        //}
        ////проверяются все условия ввода данных
        //bool checkAddConditions()
        //{
        //    Conditions conditions = new Conditions(productNameIsEmpty);
        //    conditions += productCategoryIsEmpty;
        //    conditions += productPriceIsEmpty;
        //    conditions += productCountIsEmpty;
        //    conditions += productCountCheckFormat;
        //    conditions += productPriceCheckFormat;
        //    bool IsOk = true;
        //    foreach (Conditions condition in conditions.GetInvocationList())
        //    {
        //        if (condition() == false)
        //        {
        //            IsOk = false;
        //            break;
        //        }
        //    }
        //    return IsOk;
        //}
        //#endregion

        //#region EDIT_PRODUCT_CONDITIONS
        ////проверяет на содержимость пустоты или пробелов поле имя продукта
        //private bool editNameIsEmpty()
        //{
        //    Regex spaces = new Regex("^\\s*$");
        //    if (editName.Text == string.Empty || spaces.IsMatch(editName.Text))
        //    {
        //        //MessageBox.Show("name!!!! ne ok");
        //        return false;
        //    }
        //    else return true;
        //}
        ////проверяет на содержимость пустоты или пробелов поле категория продукта
        //private bool editCategoryIsEmpty()
        //{
        //    Regex spaces = new Regex("^\\s*$");
        //    if (editCategory.Text == string.Empty || spaces.IsMatch(editCategory.Text))
        //    {
        //        //MessageBox.Show("Cat!!!! ne ok");
        //        return false;
        //    }
        //    else return true;
        //}
        ////проверяет на содержимость пустоты или пробелов поле цена продукта
        //private bool editPriceIsEmpty()
        //{
        //    Regex spaces = new Regex("^\\s*$");
        //    if (editPrice.Text == string.Empty || spaces.IsMatch(editPrice.Text))
        //    {
        //        //MessageBox.Show("Cena!!!! ne ok");
        //        return false;
        //    }
        //    else return true;
        //}
        ////проверяет на содержимость пустоты или пробелов поле количество продуктов
        //private bool editCountIsEmpty()
        //{
        //    Regex spaces = new Regex("^\\s*$");
        //    if (editCount.Text == string.Empty || spaces.IsMatch(editCount.Text))
        //    {
        //        //MessageBox.Show("count!!! ne ok");
        //        return false;
        //    }
        //    else return true;
        //}
        ////проверяет на правильность формата поле количество продуктов
        //private bool editCountCheckFormat()
        //{
        //    Regex count = new Regex("^\\d+$");
        //    if (count.IsMatch(editCount.Text))
        //    {
        //        //MessageBox.Show("Count ne ok");
        //        return true;
        //    }
        //    else return false;
        //}
        ////проверяет на правильность формата поле цена продукта
        //private bool editPriceCheckFormat()
        //{
        //    Regex price = new Regex("^\\d+,\\d{1,2}$");
        //    if (price.IsMatch(editPrice.Text))
        //    {
        //        //MessageBox.Show("Cena ok");
        //        return true;
        //    }
        //    else return false;
        //}
        ////проверяет все эти условия
        //bool checkEditConditions()
        //{
        //    Conditions conditions = new Conditions(productNameIsEmpty);
        //    conditions += editCategoryIsEmpty;
        //    conditions += editPriceIsEmpty;
        //    conditions += editCountIsEmpty;
        //    conditions += editCountCheckFormat;
        //    conditions += editPriceCheckFormat;
        //    bool IsOk = true;
        //    foreach (Conditions condition in conditions.GetInvocationList())
        //    {
        //        if (condition() == false)
        //        {
        //            IsOk = false;
        //            break;
        //        }
        //    }
        //    return IsOk;
        //}

        //#endregion

        //#region EVENTS

        ////Проверяет формат ввода цены
        //private void priceCheck(object sender, TextChangedEventArgs e)
        //{
        //    Regex price = new Regex("^\\d+,\\d{1,2}$");
        //    if (!price.IsMatch(productPrice.Text))
        //        productPrice.BorderBrush = Brushes.Red;
        //    else productPrice.BorderBrush = Brushes.DarkGreen;
        //}
        ////Проверяет формат ввода кол-ва продуктов
        //private void countCheck(object sender, TextChangedEventArgs e)
        //{
        //    Regex price = new Regex("^\\d+$");
        //    if (!price.IsMatch(productCount.Text))
        //        productCount.BorderBrush = Brushes.Red;
        //    else productCount.BorderBrush = Brushes.DarkGreen;
        //}

        ////ДЛЯ ВКЛАДКИ ИЗМЕНЕНИЯ ПРОДУКТА
        ////Проверяет формат ввода кол-ва продуктов
        //private void editPriceCheck(object sender, TextChangedEventArgs e)
        //{
        //    Regex price = new Regex("^\\d+,\\d{1,2}$");
        //    if (!price.IsMatch(editPrice.Text))
        //        editPrice.BorderBrush = Brushes.Red;
        //    else editPrice.BorderBrush = Brushes.DarkGreen;
        //}
        ////Проверяет формат ввода кол-ва продуктов
        //private void editCountCheck(object sender, TextChangedEventArgs e)
        //{
        //    Regex price = new Regex("^\\d+$");
        //    if (!price.IsMatch(editCount.Text))
        //        editCount.BorderBrush = Brushes.Red;
        //    else editCount.BorderBrush = Brushes.DarkGreen;
        //}

        ////ДОБАВЛЯЕТ ТОВАР В БАЗУ
        //private void addProduct_but(object sender, RoutedEventArgs e)
        //{
        //    //Условия ввода данных выполнились?
        //    bool IsOk = checkAddConditions();
        //    if (IsOk)
        //    {
        //        //Создаём продукт
        //        Product product = new Product();
        //        product.productName = productName.Text;
        //        product.Category = productCategory.Text;
        //        //Записываем категорию в файл
        //        AddCategory(product.Category);
        //        //если пользователь ввёл запятую заменятся на точку
        //        if (productPrice.Text.Contains(','))
        //            productPrice.Text.Replace(',', '.');
        //        //запись данных в продукт
        //        product.productPrice = double.Parse(productPrice.Text);
        //        product.productCount = int.Parse(productCount.Text);
        //        product.productPhoto = getPhotoPath(product.productName);
        //        product.descriptionProduct = productDescription.Text;
        //        //записывает в файл продукт
        //        products.AddNew(product);
        //        //уведомление о том что продукт создан
        //        ProductView productView = new ProductView(product);
        //        productView.ShowDialog();
        //        //очистка полей
        //        clearFields();
        //        //обновление данных
        //        loadCategories();
        //        loadProducts();
        //    }
        //}
        ////Изменяет продукт
        ////БУДУ ПЕРЕДЕЛЫВАТЬ
        ////private void editProduct_but(object sender, RoutedEventArgs e)
        ////{
        ////    bool IsOk = checkAddConditions();
        ////    if (IsOk)
        ////    {
        ////        Product product = new Product();
        ////        product.productName = editName.Text;
        ////        product.Category = editCategory.Text;
        ////        AddCategory(product.Category);
        ////        if (editPrice.Text.Contains(','))
        ////            editPrice.Text.Replace(',', '.');
        ////        product.productPrice = double.Parse(editPrice.Text);
        ////        product.productCount = int.Parse(editCount.Text);
        ////        product.productPhoto = getPhotoPath(product.productName);
        ////        product.descriptionProduct = editDescription.Text;
        ////        products.AddNew(product);
        ////        ProductView productView = new ProductView(product);
        ////        productView.ShowDialog();
        ////        clearFields();
        ////        loadCategories();
        ////        loadProducts();
        ////    }
        ////}

        ////Достаёт полный путь к картинке которую загружаем для продкута

        //private void addProductPhoto_but(object sender, RoutedEventArgs e)
        //{
        //    OpenFileDialog openFile = new OpenFileDialog();
        //    openFile.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
        //    if (openFile.ShowDialog() == true)
        //    {
        //        sourcePhoto = openFile.FileName;
        //    }
        //    productPhoto.Children.Clear();
        //    productPhoto.Children.Add(createImageBox(sourcePhoto));
        //}
        ////событие для кнопки в продукте
        //private void edittProduct_but(object sender, RoutedEventArgs e)
        //{
        //    Button button = (Button)sender;
        //    loadProductForEdit(button.Name);
        //}

        //#endregion

        //#region CREATE_ELEMENTS

        ////открывает вкладку для редактирования продукта
        //private void loadProductForEdit(string id)
        //{
        //    //сокрытие таблицы для вывода товаров
        //    productsView.Visibility = Visibility.Collapsed;
        //    //раскрытие вкладки для изменения конкректного товара
        //    editProductGrid.Visibility = Visibility.Visible;
        //    //Получения конкректного продукта и изменение его данных
        //    Product product = products.getProductById(id);
        //    editName.Text = product.productName;
        //    editCategory.Text = product.Category;
        //    editPrice.Text = product.productPrice.ToString();
        //    editCount.Text = product.productCount.ToString();

        //    //выгрузка фото продукта в приложение
        //    string photoDir = System.IO.Path.Combine(System.IO.Path.GetFullPath(target), product.productName);
        //    string targetPhoto = System.IO.Path.Combine(photoDir, product.productPhoto);
        //    //products.deleteProduct(product);
        //    gridPhoto.Children.Clear();
        //    gridPhoto.Children.Add(createImageBox(targetPhoto));
        //}





        ////создание текстблока
        //private TextBlock createTextBlock(string text)
        //{
        //    TextBlock textBlock = new TextBlock();
        //    textBlock.Text = text;
        //    textBlock.HorizontalAlignment = HorizontalAlignment.Center;
        //    textBlock.VerticalAlignment = VerticalAlignment.Center;
        //    textBlock.Foreground = SetColor("#15531C");
        //    textBlock.FontSize = 20;
        //    return textBlock;
        //}

        ////загрузка фото
        //BitmapImage loadPhoto(string path)
        //{
        //    BitmapImage img = new BitmapImage();
        //    if (File.Exists(path))
        //    {
        //        img.BeginInit();
        //        img.UriSource = new Uri(path);
        //        img.DecodePixelWidth = 120;
        //        img.DecodePixelHeight = 120;
        //        img.EndInit();
        //        return img;
        //    }
        //    return null;
        //}

        ////создание имаджбокса
        //Image createImageBox(string photoPath)
        //{
        //    Image img = new Image();
        //    img.Source = loadPhoto(photoPath);
        //    return img;
        //}

        ////возвращает 16-ричный цвет
        //SolidColorBrush SetColor(string hex)
        //{
        //    return (SolidColorBrush)(new BrushConverter().ConvertFrom(hex));
        //}

        ////Динамическое создание объекта продукт
        ////Параметры: продукт, цвет фона, цвет текста
        //Border CreateProduct(Product product, SolidColorBrush gridColor, SolidColorBrush textColor)
        //{
        //    int ROWS_COUNT = 6;
        //    int COLUMNS_COUNT = 2;

        //    //Создание рамки. Для скругления углов
        //    Border border = new Border();
        //    border.CornerRadius = new CornerRadius(8);
        //    border.Background = gridColor;
        //    border.Height = 260;
        //    border.Width = 180;
        //    border.Margin = new Thickness(10);

        //    //Эффект тени для рамки
        //    DropShadowEffect shadowEffect = new DropShadowEffect();
        //    shadowEffect.BlurRadius = 8;
        //    shadowEffect.Opacity = 0.5;
        //    border.Effect = shadowEffect;

        //    //Создание сетки
        //    Grid myGrid = new Grid();
        //    myGrid.Height = 260;
        //    myGrid.Width = 180;
        //    //показать линии сетки
        //    //myGrid.ShowGridLines = true;

        //    //Создание строк для сетки
        //    RowDefinition[] rows = new RowDefinition[ROWS_COUNT];
        //    for (int i = 0; i < rows.Length; i++)
        //        rows[i] = new RowDefinition();

        //    //Создание столбцов для сетки
        //    ColumnDefinition[] columns = new ColumnDefinition[COLUMNS_COUNT];
        //    for (int i = 0; i < columns.Length; i++)
        //        columns[i] = new ColumnDefinition();

        //    //добавялем столбцы в сетку
        //    myGrid.ColumnDefinitions.Add(columns[0]);
        //    myGrid.ColumnDefinitions.Add(columns[1]);

        //    //меняем высоту первой строки
        //    rows[1].Height = new GridLength(120);
        //    rows[5].Height = new GridLength(50);
        //    //добавляем строки в сетку
        //    myGrid.RowDefinitions.Add(rows[0]);
        //    myGrid.RowDefinitions.Add(rows[1]);
        //    myGrid.RowDefinitions.Add(rows[2]);
        //    myGrid.RowDefinitions.Add(rows[3]);
        //    myGrid.RowDefinitions.Add(rows[4]);
        //    myGrid.RowDefinitions.Add(rows[5]);

        //    //Фото товара
        //    Grid photo = new Grid();
        //    photo.Height = 120;
        //    photo.Width = 120;

        //    //Категория
        //    Border categoryBorder = new Border();
        //    categoryBorder.Background = SetColor("#15531C");
        //    categoryBorder.HorizontalAlignment = HorizontalAlignment.Center;
        //    categoryBorder.CornerRadius = new CornerRadius(8);
        //    categoryBorder.Margin = new Thickness(2);

        //    TextBlock category = new TextBlock();
        //    if (product.Category != String.Empty)
        //        category.Text = product.Category;
        //    else
        //        category.Text = "#CATEGORY#";

        //    category.Margin = new Thickness(2);
        //    category.FontSize = 12;
        //    category.TextWrapping = TextWrapping.Wrap;
        //    category.VerticalAlignment = VerticalAlignment.Center;
        //    category.TextAlignment = TextAlignment.Center;
        //    category.Foreground = textColor;

        //    categoryBorder.Child = category;
        //    //Добавляю в строку
        //    Grid.SetRow(categoryBorder, 0);
        //    //Растягиваю на два столбца
        //    Grid.SetColumnSpan(categoryBorder, 2);
        //    //Добавляю текст в сетку
        //    myGrid.Children.Add(categoryBorder);

        //    string phFile = System.IO.Path.GetFullPath(target);
        //    phFile = System.IO.Path.Combine(phFile, product.productName);
        //    phFile = System.IO.Path.Combine(phFile, product.productPhoto);
        //    //Загрузка фото
        //    photo.Children.Clear();
        //    if (File.Exists(phFile))
        //    {
        //        var productPhoto = loadPhoto(phFile);
        //        Image imageBox = new Image();
        //        imageBox.Source = productPhoto;
        //        photo.Children.Add(imageBox);
        //    }
        //    else
        //    {
        //        TextBlock photoInfo = new TextBlock();
        //        photoInfo.Text = "#PHOTO#";
        //        photo.Background = SetColor("#1F1F1F");
        //        photo.Children.Add(photoInfo);
        //    }
        //    Grid.SetRow(photo, 1);
        //    Grid.SetColumnSpan(photo, 2);
        //    myGrid.Children.Add(photo);

        //    //Количество товаров
        //    TextBlock itemsCount = new TextBlock();
        //    itemsCount.Text = $"Items count: {product.productCount}";
        //    itemsCount.FontSize = 12;
        //    itemsCount.TextWrapping = TextWrapping.Wrap;
        //    itemsCount.VerticalAlignment = VerticalAlignment.Center;
        //    itemsCount.TextAlignment = TextAlignment.Center;
        //    itemsCount.Foreground = textColor;
        //    //Добавляю в строку
        //    Grid.SetRow(itemsCount, 2);
        //    //Растягиваю на два столбца
        //    Grid.SetColumnSpan(itemsCount, 2);
        //    //Добавляю текст в сетку
        //    myGrid.Children.Add(itemsCount);

        //    //Название товара
        //    TextBlock productName = new TextBlock();
        //    if (product.productName != String.Empty)
        //        productName.Text = product.productName;
        //    else productName.Text = "#PRODUCT_NAME#";
        //    productName.FontSize = 15;
        //    productName.TextWrapping = TextWrapping.Wrap;
        //    productName.VerticalAlignment = VerticalAlignment.Center;
        //    productName.TextAlignment = TextAlignment.Center;
        //    productName.Foreground = textColor;
        //    //Добавляю в строку
        //    Grid.SetRow(productName, 3);
        //    //Растягиваю на два столбца
        //    Grid.SetColumnSpan(productName, 2);
        //    //Добавляю текст в сетку
        //    myGrid.Children.Add(productName);

        //    //Цена товара
        //    TextBlock price = new TextBlock();
        //    price.Text = $"{product.productPrice} grn";
        //    price.TextAlignment = TextAlignment.Center;
        //    price.VerticalAlignment = VerticalAlignment.Center;
        //    price.FontSize = 15;
        //    price.Foreground = textColor;
        //    Grid.SetRow(price, 4);
        //    Grid.SetColumnSpan(price, 2);
        //    myGrid.Children.Add(price);

        //    Border buyBorder = new Border();
        //    buyBorder.Background = SetColor("#15531C");
        //    buyBorder.CornerRadius = new CornerRadius(8);
        //    buyBorder.BorderThickness = new Thickness(1);
        //    buyBorder.Margin = new Thickness(10);

        //    //Кнопка купить
        //    Button editBut = new Button();
        //    editBut.BorderThickness = new Thickness(0);
        //    if (product.Id != string.Empty)
        //        editBut.Name = product.Id;
        //    editBut.Content = "Edit";
        //    editBut.Foreground = Brushes.White;
        //    editBut.Background = SetColor("#15531C");
        //    editBut.Margin = new Thickness(2);
        //    editBut.Foreground = Brushes.White;
        //    //СОБЫТИЕ КОТОРОЕ ПОЗВОЛЯЕТ ПОНЯТЬ ПРОГРАММЕ С КАКИМ ПРОДУКТОМ ЕЙ РАБОТАТЬ
        //    //НЕ ЗАКОНЧЕНО
        //    //editBut.Click += new RoutedEventHandler(edittProduct_but);
        //    buyBorder.Child = editBut;
        //    Grid.SetRow(buyBorder, 5);
        //    Grid.SetColumnSpan(buyBorder, 2);
        //    myGrid.Children.Add(buyBorder);



        //    //добавляю в рамку сетку
        //    border.Child = myGrid;
        //    return border;
        //}

        //#endregion
    }
}
