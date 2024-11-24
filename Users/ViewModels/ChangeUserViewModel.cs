using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive;
using Users.Common;
using Users.Common.IoC;

namespace Users.ViewModels
{
    public class ChangeUserViewModel : ReactiveObject
    {
        public string[] AccessLevels { get; set; } = { "Guest", "User", "Moderator", "Administrator" };
        public string SelectedLevel { get; set; } = string.Empty;

        public ReactiveCommand<UserVM, Unit> ChangeUserCommand { get; set; }
        public ReactiveCommand<Unit, Unit> CloseCommand { get; }

        IServiceView _serviceView;
        IUserRepository _userRepository; 
        public UserVM UserVM { get; set; }
        public ChangeUserViewModel(IServiceView serviceView, IUserRepository userRepository, UserVM userVM)
        {
            _serviceView = serviceView;
            _userRepository = userRepository;
            UserVM = userVM;
            SelectedLevel = userVM.AccessLevel;

            ChangeUserCommand = ReactiveCommand.Create<UserVM>(ChangeUser);
            CloseCommand = ReactiveCommand.Create(Close);
        }

        async void ChangeUser(UserVM userVM)
        {
            userVM.AccessLevel = SelectedLevel;
            if(await _userRepository.ChangeUser(userVM)) Close();
        }
        void Close() => _serviceView.Close<ChangeUserViewModel>();
    }
}