using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataLayer.Model;
using Microsoft.Maui.ApplicationModel.Communication;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Welcome.Others;

namespace MAUI.ViewModels
{
    [QueryProperty(nameof(LoggedUserRole), "role")]
    public partial class AddUserViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private string _role;

        [ObservableProperty]
        private UserRoleEnum _roleEnum;

        [ObservableProperty]
        private string _studentNumber;

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private DateTime _birthDay;

        [ObservableProperty]
        private DateTime _expires;

        [ObservableProperty]
        private ObservableCollection<AddUserViewModel> _users;

        [ObservableProperty]
        private bool _isMoreButtonVisible = false;

        [ObservableProperty]
        private string _loggedUserRole;

        private DatabaseUser _user;


        public AddUserViewModel()
        {
            _user = new DatabaseUser();
            _users = new ObservableCollection<AddUserViewModel>();

            //CheckCondition();

        }

        [RelayCommand]
        public async Task AddUser()
        {

            _user.Email = Email;
            _user.Expires = Expires;
            _user.Role = GetUserRoleFromString(Role);
            _user.BirthDay = BirthDay;
            _user.Name = Name;
            _user.Password = Password;
            _user.StudentNumber = StudentNumber;

            bool result = _user.AddUser(_user);

            if (result)
            {
                await Application.Current.MainPage.DisplayAlert("Succsessfully !", "You have succsessfully created a user!", "Back");

                Email = string.Empty;
                Name = string.Empty;
                Password = string.Empty;
                StudentNumber = string.Empty;
                Email = string.Empty;
                BirthDay = DateTime.MinValue;
                Expires = DateTime.MinValue;
                Role = null;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error !", "There was an error !", "Back");
            }
        }

        [RelayCommand]
        public async Task AllUsers()
        {
            var users = _user.GetAllUsers();

            var newUsers = new ObservableCollection<AddUserViewModel>();

            foreach (var user in users)
            {
                AddUserViewModel vm = new AddUserViewModel()
                {
                    Name = user.Name,
                    Email = user.Email,
                    Expires = user.Expires,
                    BirthDay = user.BirthDay,
                    Password = _user.Encrypt(user.Password),
                    StudentNumber = user.StudentNumber,
                    RoleEnum = user.Role
                };


                newUsers.Add(vm);
            }

            if(newUsers.Count >0) IsMoreButtonVisible = true;

            Users = newUsers;
        }

        [RelayCommand]
        public async Task ToLogin()
        {
            await Shell.Current.GoToAsync("//Login");
        }

        [RelayCommand]
        public async Task RoleChanged() 
        {
            var users = _user.GetByRole(Role);

            var newModel = new ObservableCollection<AddUserViewModel>();

            foreach (var user in users)
            {
                AddUserViewModel vm = new AddUserViewModel()
                {
                    Name = user.Name,
                    Email = user.Email,
                    Expires = user.Expires,
                    BirthDay = user.BirthDay,
                    Password = _user.Encrypt(user.Password),
                    StudentNumber = user.StudentNumber,
                    RoleEnum = user.Role
                };


                newModel.Add(vm);
            }


            Users = newModel;
        }

        [RelayCommand]
        public async Task ToLogs() 
        {
            await Shell.Current.GoToAsync("//Logs");
        }

        private UserRoleEnum GetUserRoleFromString(string roleString)
        {
            switch (roleString.ToLower())
            {
                case "1":
                    return UserRoleEnum.ADMIN;
                case "2":
                    return UserRoleEnum.PROFESSOR;
                case "3":
                    return UserRoleEnum.INSPECTOR;
            }

            return UserRoleEnum.STUDENT;
        }

    }
}
