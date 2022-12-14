using Next_Level.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Next_Level.Classes
{
    public class ProductList
    {
        public List<Product> products { get; set; }
        IFile file = null;
        public bool fileLoad { get; set; } = false; 
        string product_path = NextLevelPath.PRODUCT_PATH;

        public ProductList()
        {
            products = Load();
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
                if (!idIsUnique(result))
                    break;
            }

            return result;
        }

        public void AddNew(Product product)
        {
            product.Id = "nl" + generateId();
            products.Add(product);
            Save();
        }
        public void removeUser(Product product)
        {
            products.Remove(product);
            Save();
        }

        public void deleteProduct(Product product)
        {
            products.Remove(product);
            Save();
        }
        //Возвращает список товаров которые содержат похожие имена
        public List<Product> getProductByName(string productName)
        {
            List<Product> find = new List<Product>();
            foreach (var product in products)
            {
                if (product.productName.Contains(productName))
                    find.Add(product);
            }
            if (find.Count != 0)
                return find;
            else return null;
        }

        public List<Product> getProductsByPrice(double productPrice)
        {
            List<Product> find = new List<Product>();
            foreach (var product in products)
            {
                if (product.productPrice <= productPrice)
                    find.Add(product);
            }
            if (find.Count != 0)
                return find;
            else return null;
        }
        public bool isHaveCategory(string category)
        {
            foreach(var product in products)
            {
                if (product.Category == category)
                    return true;
            }
            return false;
        }
        public List<Product> getProductsByCategory(string category)
        {
            List<Product> find = new List<Product>();
            foreach (var product in products)
            {
                if (product.Category == category)
                    find.Add(product);
            }
            if (find.Count != 0)
                return find;
            else return null;
        }

        public Product getProductById(string productId)
        {
            foreach (var product in products)
            {
                if (product.Id == productId)
                    return product;
            }
            return null;
        }
        public List<Product> getByLike()
        {
            List<Product> find = new List<Product>();
            foreach (var product in products)
            {
                if (product.Liked == true)
                    find.Add(product);

                
            }
            if(find.Count != 0)
                return find;

            return null;
            
        }

        public bool idIsUnique(string id)
        {
            foreach (var product in products)
            {
                if (product.Id == id)
                    return true;
            }
            return false;
        }

        public IEnumerator<Product> GetEnumerator()
        {
            for (int i = 0; i < products.Count; i++)
                yield return products[i];
        }
        void Save()
        {
            file = new XmlFormat(product_path);
            file.Save(products);
        }
        List<Product> Load()
        {
            if (File.Exists(product_path))
            {
                file = new XmlFormat(product_path);
                fileLoad = true;
                return file.Load<List<Product>>();
            }
            fileLoad = false;
            return new List<Product>();
        }
    }
}
