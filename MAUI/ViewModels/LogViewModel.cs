using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.ViewModels
{
    public partial class LogViewModel : ObservableObject
    {

        [ObservableProperty]
        private string _date;

        [ObservableProperty]
        private string _message;

        [ObservableProperty]
        private ObservableCollection<LogViewModel> _logs;

        private DatabaseUser _user;


        public LogViewModel()
        {
            Date = string.Empty;
            Message = string.Empty;
            _user = new DatabaseUser();

            //UpdateLogs();
        }


        [RelayCommand]

        public async Task UpdateLogs()
        {
            var results = _user.GetLogMessages();

            ObservableCollection<LogViewModel> logViewModels = new ObservableCollection<LogViewModel>();

            foreach (var item in results)
            {

                LogViewModel logViewModel = new LogViewModel();
                logViewModel.Date = item.Key;
                logViewModel.Message = item.Value;

                logViewModels.Add(logViewModel);
            }

            Logs = logViewModels;
        }

        [RelayCommand]
        public async Task ShowLogDetails(string logMessage)
        {
            await Application.Current.MainPage.DisplayAlert("Log Details", logMessage, "Back");
        }

        [RelayCommand]
        public async Task ToLogin()
        {
            await Shell.Current.GoToAsync("//Login");

        }

        [RelayCommand]
        public async Task ToStudentsList() 
        {
            await Shell.Current.GoToAsync("//StudentsList");
        }
    }
}
