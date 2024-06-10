using Welcome.Model;
using Welcome.Others;

namespace Welcome.ViewModel
{
    public class UserViewModel
    {
        private User _user;

        public UserViewModel(User user)
        {
            _user = user;
        }

        public string Name
        {
            get { return _user.Name; }
            set { _user.Name = value; }
        }

        public string Password 
        {
            get { return _user.Password; }
            set { _user.Password = value; }
        }

        public UserRoleEnum Role 
        {
            get { return _user.Role; }
            set { _user.Role = value; }
        }

        public string Email 
        {
            get { return _user.Email;  }
            set { _user.Email = value; }
        }

        public string StudentNumber 
        {
            get { return _user.StudentNumber; }
            set { _user.StudentNumber = value; }
        }

        public DateTime BirthDay 
        {
            get { return _user.BirthDay; }
            set { _user.BirthDay = value; }
        }
    }
}
