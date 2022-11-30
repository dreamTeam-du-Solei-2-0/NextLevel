using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Next_Level.Classes
{
    [Serializable]
    class SaveLogin
    {
        public bool save_Data { get; set; }
        public string login { get; set; }
        public string password { get; set; }
    }
}
