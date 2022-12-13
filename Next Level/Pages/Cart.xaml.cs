using Next_Level.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Next_Level.Pages
{
    public partial class Cart : Page
    {
        ProductList products;
        string current_id = Order._id;
        public Cart()
        {
            InitializeComponent();
            products.getProductById(current_id);

        }

       
    }
}
