using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using Users.Common;
using Users.Common.IoC;
using Users.Models;

namespace Users.ViewModels
{
    public class AddUserViewModel : ReactiveObject
    {
        public UserVM UserVM { get; set; } = new();
        public string[] AccessLevels { get; set; } = { "Guest", "User", "Moderator", "Administrator" };
        public string SelectedLevel { get; set; } = "Guest";

        public ReactiveCommand<UserVM,Unit> AddUserCommand { get; set; }
        public ReactiveCommand<Unit, Unit> CloseCommand { get; }

        IServiceView _serviceView;
        IUserRepository _userRepository;
        ObservableCollection<UserVM> _users;
        public AddUserViewModel(IServiceView serviceView, IUserRepository userRepository, ObservableCollection<UserVM> users)
        {
            _serviceView = serviceView;
            _userRepository = userRepository;
            _users = users;

            AddUserCommand = ReactiveCommand.Create<UserVM>(AddUser);
            CloseCommand = ReactiveCommand.Create(Close);
        }

        async void AddUser(UserVM userVM)
        {
            var user = new User
            {
                Id = userVM.Id,
                SurName = userVM.SurName,
                Name = userVM.Name,
                Login = userVM.Login,
                Password = userVM.Password,
                Mail = userVM.Mail,
                AccessLevel = SelectedLevel,
                Notes = userVM.Notes
            };

            if (await _userRepository.AddUser(user))
            {
                _users.Add(userVM);
                _serviceView.Close<AddUserViewModel>();
            }
        }
        void Close() => _serviceView.Close<AddUserViewModel>();
    }
}