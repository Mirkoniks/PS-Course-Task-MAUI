using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataLayer.DataBase;
using DataLayer.Model;
using Microsoft.Maui.Controls;
//using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Welcome.Others;
using WelcomeExtended.Others;

namespace MAUI.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _password;

        private DatabaseUser _user;

        public LoginViewModel()
        {
            _user = new DatabaseUser();
            Name = string.Empty;
            Password = string.Empty;
        }


        [RelayCommand]
        private async Task Login() 
        {

            bool result = _user.LogUser(Name, Password);
            if (!result)
            {
                await Application.Current.MainPage.DisplayAlert("Authentication error", "Wrong password or username", "Back");
                Delegates.LogLoginError(Name);


            }
            else 
            {        
                Delegates.LogLoginSuccess(Name);
                
                await Shell.Current.GoToAsync($"//StudentsList");

            }
        }
    }
}
