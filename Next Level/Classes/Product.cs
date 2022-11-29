using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml.Permissions;
using Next_Level.Classes;

namespace Next_Level.Classes
{   
    [Serializable]
    public class Product
    {
        string path;

        

        public string Name { get; set; }

        public double NewPrice { get; set; }

        public double OldPrice { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public Dictionary<int, string> Comments { get; set; }

        public bool isAvailable { get; set; }

        public int Views  { get; set; }

        public int FrequentlyPurchasedCounter { get; set; }

        public DateTime PlacementDate { get; set; }

        public int ID_Product { set; get; }

        public Product()
        {

            ID_Product++;

        }
        

        public void InFile() {
            path = ID_Product.ToString();
            XmlFormat product_xml = new XmlFormat(path);
            
        }


        public Product(string name, double new_price, string image)
        {
            
            this.Name = name;
            this.NewPrice = new_price;
            this.Image = image;
        }
    }
}
