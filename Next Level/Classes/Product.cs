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
        public string Id { get; set; }
        public string Category { get; set; }
        public string productPhoto { get; set; }
        public string productName { get; set; }
        public string descriptionProduct { get; set; }
        public double productPrice { get; set; }
        public int productCount { get; set; }
        public int currentCount { get; set; }
        public List<Feedback> coments { get; set; }
        public string customer { get; set; }
        public bool Liked { get; set; }

        public Product()
        {
            Id = string.Empty;
            productPhoto = string.Empty;
            productName = string.Empty;
            descriptionProduct = string.Empty;
            Category = string.Empty;
            customer = string.Empty;
            productPrice = 0;
            productCount = 0;
            currentCount = 0;
            coments = new List<Feedback>();
        }
    }

    #region before_edit
    //[Serializable]
    //public class Product
    //{
    //    string path;



    //    public string Name { get; set; }

    //    public double NewPrice { get; set; }

    //    public double OldPrice { get; set; }

    //    public string Description { get; set; }

    //    public string Image { get; set; }

    //    public Dictionary<int, string> Comments { get; set; }

    //    public bool isAvailable { get; set; }

    //    public int Views  { get; set; }

    //    public int FrequentlyPurchasedCounter { get; set; }

    //    public DateTime PlacementDate { get; set; }

    //    public int ID_Product { set; get; }

    //    public Product()
    //    {

    //        ID_Product++;

    //    }



    //}
    #endregion
}
