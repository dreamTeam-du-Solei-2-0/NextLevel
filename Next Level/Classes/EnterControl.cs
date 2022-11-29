using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Next_Level.Classes
{
    static class EnterControl
    {
        static Regex regex = null;
        static public bool CheckPass(string password)
        {
            regex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[_#?!@$%^&*№<>{}~-]).{8,}$");
            if (regex.IsMatch(password))
                return true;
            else return false;
        }
        static public bool CheckEmail(string email)
        {
            regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (regex.IsMatch(email))
                return true;
            else return false;
        }
    }
}
