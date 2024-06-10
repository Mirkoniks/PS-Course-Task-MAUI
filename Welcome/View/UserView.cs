using Welcome.ViewModel;

namespace Welcome.View
{
    public class UserView
    {
        private UserViewModel _viewModel;

        public UserView(UserViewModel userViewModel)
        {
            _viewModel = userViewModel;
        }

        public void Display() 
        {
            Console.WriteLine("Welcome: ");
            Console.WriteLine($"Name {_viewModel.Name}");
            Console.WriteLine($"Name {_viewModel.Role}");
        }

        public void DisplayFull() 
        {
            Console.WriteLine("Welcome: ");
            Console.WriteLine($"Name {_viewModel.Name}");
            Console.WriteLine($"Name {_viewModel.Role}");
            Console.WriteLine($"Name {_viewModel.Email}");
            Console.WriteLine($"Name {_viewModel.BirthDay}");
            Console.WriteLine($"Name {_viewModel.StudentNumber}");
        }

        public void DisplayError() 
        {
            throw new ArgumentException("Error HERE!!!");
        }
    }
}
