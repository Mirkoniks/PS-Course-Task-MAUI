using DataLayer.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Welcome.Model;
using Welcome.Others;

namespace DataLayer.Model
{
    public class DatabaseUser : User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public DatabaseUser()
        {
            
        }


        public bool LogUser(string username, string password)
        {
            using (var db = new DatabaseContext())
            {
                var user = db.Users.Where(u => u.Name == username).FirstOrDefault();


                if (user != null)
                {
                    Role = user.Role;

                    if (user.Password == password)
                    {

                        return true;

                    }
                }
                return false;
            }
        }

        public bool AddUser(DatabaseUser user) 
        {
            using (var context = new DatabaseContext())
            {

                var userToBeCreated = new DatabaseUser()
                {
                    Name = user.Name,
                    Password = user.Password,
                    Role = user.Role,
                    Expires = user.Expires,
                    BirthDay = user.BirthDay,
                    Email = user.Email,
                    StudentNumber = user.StudentNumber
                };

                context.Add<DatabaseUser>(userToBeCreated);
                var result = context.SaveChanges() > 0;

                return result;
            }
        }

        public bool RemoveUser(string name) 
        {
            using (var context = new DatabaseContext())
            { 
                var userToRemove = context.Users.FirstOrDefault(u => u.Name == name);

                if (userToRemove == null)
                {
                    return false;
                }

                context.Users.Remove(userToRemove);

                return context.SaveChanges() > 0;
            }
        }

        public List<DatabaseUser> GetAllUsers()
        {
            using (var context = new DatabaseContext())
            {
                var records = context.Users.ToList();

                return records;
            }
        }

        public List<DatabaseUser> GetByRole(string role)
        {
            using (var context = new DatabaseContext())
            {
                List<DatabaseUser> records = new List<DatabaseUser>();

                switch (role)
                {
                    case "0":
                        records = context.Users.Where(u => u.Role == UserRoleEnum.ADMIN).ToList();
                        break;
                    case "3":
                        records = context.Users.Where(u => u.Role == UserRoleEnum.STUDENT).ToList();
                        break;
                    case "2":
                        records = context.Users.Where(u => u.Role == UserRoleEnum.INSPECTOR).ToList();
                        break;
                    case "1":
                        records = context.Users.Where(u => u.Role == UserRoleEnum.PROFESSOR).ToList();
                        break;
                }

                return records;
            }
        }


        public List<KeyValuePair<string, string>> GetLogMessages()
        {
            using (var context = new DatabaseContext())
            {
                var logEvents = context.LogMessages.ToList();

                logEvents.Reverse();

                List<KeyValuePair<string, string>> messages = new List<KeyValuePair<string, string>>(); 

                foreach (var logEvent in logEvents)
                {
                    var a = new KeyValuePair<string, string>(logEvent.CreatedOn.ToString("dd - MM - yyyy"), logEvent.Message);

                    messages.Add(a);
                }

                return messages;
            }
        }

        public UserRoleEnum GetRoleByName(string name) 
        {
            using (var context = new DatabaseContext()) 
            {
                var res = context.Users.Where(u => u.Name == name).FirstOrDefault();


                if (res != null) 
                {
                    return res.Role;
                }

                return UserRoleEnum.ANONYMUS;
            }
        }
    }
}
