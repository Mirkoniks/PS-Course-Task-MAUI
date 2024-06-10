using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Welcome.Model;
using Welcome.Others;

namespace WelcomeExtended.Data
{
    public class UserData
    {
        private List<User> _users;

        private int _nextId;

        public UserData()
        {
            _nextId = 0;
            _users = new List<User>();
        }

        public void AddUser(User user) 
        {
            _users.Add(user);
            _nextId++;
        }

        public void DeleteUser(User user) 
        {
            _users.Remove(user);
        }

        public bool ValidateUser(string name, string password) 
        {
            foreach (var user in _users) 
            {
                if (user.Name == name && user.Password == password) 
                {
                    return true;
                }
            }

            return false;
        }

        public bool ValidateUserLabmda(string name, string password) 
        {
            return _users.Where(x => x.Name == name && x.Password == password)
                .FirstOrDefault() != null ? true : false;
        }

        public bool ValidateUserLinq(string name, string password) 
        {
            var ret = from user in _users
                      where user.Name == name && user.Password == password
                      select user.Id;

            return ret!= null ? true : false;
        }

        public User GetUser(string name, string password) 
        {
            return  _users.Where(u => u.Name == name && u.Password == password).FirstOrDefault();
        }

        public void SetActive(string name, DateTime dataTime) 
        {
            _users.Where(u => u.Name == name).FirstOrDefault().Expires = dataTime; 
        }

        public void AssignRole(string name, UserRoleEnum role) 
        {
            _users.Where(u => u.Name == name).FirstOrDefault().Role = role;
        }
    }
}
