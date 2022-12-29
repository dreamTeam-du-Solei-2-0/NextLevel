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
        string product_path;

        public ProductList()
        {
            product_path = NextLevelPath.PRODUCT_PATH;
            products = Load();
        }
        public ProductList(string path)
        {
            product_path = path;
            products = Load();
        }
       

        public void AddNew(Product product)
        {
            products.Add(product);
            Save();
        }
        public void removeProduct(Product product)
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

        public List<Product> getProductsByCustomer(string customer)
        {
            List<Product> find = new List<Product>();
            foreach (var product in products)
            {
                if (product.customer==customer)
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
        public Product getProductByIdAndCustomer(string productId,string customer)
        {
            foreach (var product in products)
            {
                if (product.Id == productId && product.customer == customer) 
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
            if (products.Count != 0)
            {
                foreach (var product in products)
                {
                    if (product.Id == id)
                        return false;
                }
                return true;
            }
            return true;
        }

        public IEnumerator<Product> GetEnumerator()
        {
            for (int i = 0; i < products.Count; i++)
                yield return products[i];
        }
        public void Save()
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
