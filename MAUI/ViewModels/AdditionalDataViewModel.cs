using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Welcome.Others;

namespace MAUI.ViewModels
{
    public partial class AdditionalDataViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private DateTime _expires;

        [ObservableProperty]
        private UserRoleEnum _roleEnum;

        [ObservableProperty]
        private ObservableCollection<AdditionalDataViewModel> _users;

        private DatabaseUser _user;

        [ObservableProperty]
        private bool _areVisible = false;

        public AdditionalDataViewModel()
        {
            _user = new DatabaseUser();
        }

        [RelayCommand]
        public async Task LoadAditionalData() 
        {
            var users = _user.GetAllUsers();

            var newUsers = new ObservableCollection<AdditionalDataViewModel>();

            foreach (var user in users)
            {
                AdditionalDataViewModel vm = new AdditionalDataViewModel()
                {
                    Expires = user.Expires,
                    Password = _user.Encrypt(user.Password),
                    RoleEnum = user.Role
                };


                newUsers.Add(vm);
            }

            if(newUsers.Count > 0) AreVisible = true;

            Users = newUsers;
        }
    }
}
