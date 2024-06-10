using CommunityToolkit.Mvvm.ComponentModel;
using MAUI.ViewModels;

namespace MAUI.ViewModels
{
    public partial class CombinedViewModel : ObservableObject
    {
        public AddUserViewModel AddUserVM { get; set; }
        public AdditionalDataViewModel AdditionalDataVM { get; set; }

        public CombinedViewModel()
        {
            AddUserVM = new AddUserViewModel();
            AdditionalDataVM = new AdditionalDataViewModel();
        }
    }
}
