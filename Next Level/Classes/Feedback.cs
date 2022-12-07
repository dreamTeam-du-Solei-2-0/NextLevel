using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Next_Level.Classes
{
    [Serializable]
    public class Feedback
    {
        public string username { get; set; }
        public string comment { get; set; }
        public string date { get; set; }

        public Feedback()
        {
            username = string.Empty;
            comment = string.Empty;
            date = string.Empty;
        }
    }
}
