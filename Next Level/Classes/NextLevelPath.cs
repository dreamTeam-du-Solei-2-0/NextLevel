using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Next_Level.Classes
{
    public static class NextLevelPath
    {
        public static string ACCOUNT_PATH { get; } = @"..\Debug\Accounts.bin";
        public static string PRODUCT_PATH { get; } = @"..\Debug\Products.xml";
        public static string CURRENT_USER { get; } = @"..\Debug\CurrentLogin.bin";
        public static string SAVE_LOGIN { get; } = @"..\Debug\SaveLogin.bin";
        public static string PROJECT_PATH { get; } = @"..\Debug";
        public static string STOREBD_PATH { get; } = @"..\Debug\bd";
        public static string CATEGORIES_PATH { get; } = @"..\Debug\Categories.xml";
        public static string CART_PATH { get; }= @"..\Debug\Cart.xml";
    }
}
