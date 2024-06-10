using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataLayer.DataBase;
using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.ViewModels
{
    public partial class RemoveUserViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _name;

        private DatabaseUser _user;

        public RemoveUserViewModel()
        {
            _user = new DatabaseUser();    
        }

        [RelayCommand]
        public async Task RemoveUser() 
        {
            string userName = _name;

            if (string.IsNullOrWhiteSpace(userName))
            {
                await Application.Current.MainPage.DisplayAlert("Error !", "User not found.", "Back");
                return;
            }

            var result = _user.RemoveUser(Name);

            if (result)
            {
                await Application.Current.MainPage.DisplayAlert("Succsess !", "User removed successfully!", "Back");
                Name = string.Empty;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error !", "Failed to remove user.", "Back");
            }

        }
    }
}
