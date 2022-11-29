using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Next_Level.Classes
{
    [Serializable]
    public class User
    {
        public bool isAdmin { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDayDate { get; set; }  
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
