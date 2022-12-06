using Next_Level.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Next_Level.Classes
{
    public class Accounts
    {
        IFile file = new BinnaryFile(NextLevelPath.ACCOUNT_PATH);
        public List<User> account { get; set; } = new List<User>();
        public Accounts() => Load();
        public void AddNew(User person)
        {
            account.Add(person);
            Save();
        }
        public void removeUser(User person)
        {
            account.Remove(person);
            Save();
        }
        public User getUserByLogin(string login)
        {
            foreach (var person in account)
            {
                if (person.Login == login)
                    return person;
            }
            return null;
        }
        public bool EmailIsExist(string email)
        {
            foreach (var person in account)
            {
                if (person.Email == email)
                    return true;
            }
            return false;
        }

        public bool LoginIsExist(string login)
        {
            foreach (var person in account)
            {
                if (person.Login == login)
                    return true;
            }
            return false;
        }

        public bool CheckPassword(string login, string password)
        {
            foreach (var person in account)
            {
                if (person.Login == login)
                {
                    if (person.Password == password)
                        return true;
                }

            }
            return false;
        }
        public void Save()
        {
            file.Save<List<User>>(account);
        }
        public void Load()
        {
            if (file.Load<List<User>>() != null)
                account = file.Load<List<User>>();
        }
        public IEnumerator<User> GetEnumerator()
        {
            for (int i = 0; i < account.Count; i++)
                yield return account[i];
        }

    }
}
