using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Welcome.Model;

namespace WelcomeExtended.Helpers
{
    public static class UserHelper
    {
        public static bool ValidateCredentials(string name, string password)  
        {
            string field = "";

            if (name.Length == 0)
            {
                field = "name";
            }
            else if (password.Length == 0)
            {
                field = "password";
            }
            else 
            {
                return true;
            }

            throw new ArgumentException($"The {field} cannot be empry");
        }

        public static User GetUser(List<User> users, string name) 
        {
            return users.Where(u => u.Name == name).FirstOrDefault();
        }
    }
}
