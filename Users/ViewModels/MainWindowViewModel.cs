using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;
using System.Reactive;
using Users.Common;
using Users.Common.IoC;

namespace Users.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<UserVM> Users { get; set; } = new()
        {
            new UserVM() { SurName = "Васькин", Name = "Вася", Login = "1", Password = "2", Mail = "Почта.ру", AccessLevel = "Guest", Notes = "Заметки1" },
            new UserVM() { SurName = "Михайлов", Name = "Михаил", Login = "Логин1", Password = "Пароль2", Mail = "гуглпочта", AccessLevel = "Moderator", Notes = "Заметки2" },
            new UserVM() { SurName = "Викторова", Name = "Виктория", Login = "логин", Password = "пароль", Mail = "яндекспочта", AccessLevel = "Administrator", Notes = "Заметки3" }
        };
        [Reactive] public UserVM UserVM { get; set; }

        public ReactiveCommand<Unit,Unit> AddUserCommand { get; set; }
        public ReactiveCommand<UserVM, Unit> DeleteUserCommand { get; set; }
        public ReactiveCommand<UserVM, Unit> ChangeCommand { get; set; }

        IServiceView _serviceView;
        IUserRepository _userRepository;
        public MainWindowViewModel(IServiceView serviceView, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _serviceView = serviceView;

            userRepository.CreateTableUser();

            foreach(var user in userRepository.GetUsers())
            {
                var userVM = new UserVM()
                {
                    Id = user.Id,
                    SurName = user.SurName,
                    Name = user.Name,
                    Login = user.Login,
                    Password = user.Password,
                    Mail = user.Mail,
                    AccessLevel = user.AccessLevel,
                    Notes = user.Notes
                };
                Users.Add(userVM);
            }

            AddUserCommand = ReactiveCommand.Create(AddUser);
            DeleteUserCommand = ReactiveCommand.Create<UserVM>(DeleteUser);
            ChangeCommand = ReactiveCommand.Create<UserVM>(ChangeUser);
        }

        void AddUser() => _serviceView.Window<AddUserViewModel>(null, Users).NonModal();
        async void DeleteUser(UserVM userVM)
        {
            if (await _userRepository.DeleteUser(userVM.Id)) Users.Remove(userVM);
        }
        void ChangeUser(UserVM userVM)
        {
            if(userVM != null) _serviceView.Window<ChangeUserViewModel>(null, userVM).NonModal();
        }
    }
}
