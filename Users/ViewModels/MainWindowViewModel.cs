using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;
using System.Reactive;
using Users.Common;
using Users.Models;

namespace Users.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string[] AccessLevels { get; set; } = { "Guest", "User", "Moderator", "Administrator" };
        public ObservableCollection<UserVM> Users { get; set; } = new()
        {
            new UserVM() { SurName = "Васькин", Name = "Вася", Login = "1", Password = "2", Mail = "Почта.ру", AccessLevel = "Guest", Notes = "Заметки1" },
            new UserVM() { SurName = "Михайлов", Name = "Михаил", Login = "Логин1", Password = "Пароль2", Mail = "гуглпочта", AccessLevel = "Moderator", Notes = "Заметки2" },
            new UserVM() { SurName = "Викторова", Name = "Виктория", Login = "логин", Password = "пароль", Mail = "яндекспочта", AccessLevel = "Administrator", Notes = "Заметки3" }
        };
        [Reactive] public UserVM UserVM { get; set; }

        [Reactive] public bool Visibility { get; set; }
        [Reactive] public bool VisibilityButtonAdd { get; set; }
        [Reactive] public bool VisibilityButtonChange { get; set; }


        [Reactive] public string SurName { get; set; } = string.Empty;
        [Reactive] public string Name { get; set; } = string.Empty;
        [Reactive] public string Login { get; set; } = string.Empty;
        [Reactive] public string Password { get; set; } = string.Empty;
        [Reactive] public string Mail { get; set; } = string.Empty;
        [Reactive] public string AccessLevel { get; set; } = string.Empty;
        [Reactive]  public string Notes { get; set; } = string.Empty;


        public ReactiveCommand<Unit,Unit> UserCommand { get; set; }
        public ReactiveCommand<UserVM, Unit> DeleteUserCommand { get; set; }
        public ReactiveCommand<UserVM, Unit> ChangeCommand { get; set; }

        public ReactiveCommand<Unit,Unit> CancelCommand { get; set; }
        public ReactiveCommand<Unit,Unit> AddUserCommand { get; set; }
        public ReactiveCommand<UserVM, Unit> ChangeUserCommand { get; set; }

        IUserRepository _userRepository;
        public MainWindowViewModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;

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

            UserCommand = ReactiveCommand.Create(User);
            DeleteUserCommand = ReactiveCommand.Create<UserVM>(DeleteUser);
            ChangeCommand = ReactiveCommand.Create<UserVM>(Change);

            CancelCommand = ReactiveCommand.Create(Cancel);
            AddUserCommand = ReactiveCommand.Create(AddUser);
            ChangeUserCommand = ReactiveCommand.Create<UserVM>(ChangeUser);
        }

        void User()
        {
            if (!Visibility)
            {
                Visibility = true;
                VisibilityButtonAdd = true;
                VisibilityButtonChange = false;
            }
        }
        async void DeleteUser(UserVM user)
        {
            if (await _userRepository.DeleteUser(user.Id))
            {
                Users.Remove(user);
            }
        }
        void Change(UserVM user) 
        {
            if (user != null)
            {
                Visibility = true;
                VisibilityButtonAdd = false;
                VisibilityButtonChange = true;

                SurName = UserVM.SurName;
                Name = UserVM. Name;
                Login = UserVM. Login;
                Password = UserVM. Password;
                Mail = UserVM. Mail;
                AccessLevel = UserVM. AccessLevel;
                Notes = UserVM. Notes;
            }
        }

        async void AddUser()
        {
            var userVM = new UserVM
            {
                SurName = SurName,
                Name = Name,
                Login = Login,
                Password = Password,
                Mail = Mail,
                AccessLevel = AccessLevel,
                Notes = Notes
            };
            var user = new User
            {
                Id = userVM.Id,
                SurName = userVM.SurName,
                Name = userVM.Name,
                Login = userVM.Login,
                Password = userVM.Password,
                Mail = userVM.Mail,
                AccessLevel = userVM.AccessLevel,
                Notes = userVM.Notes
            };
            if (await _userRepository.AddUser(user))
            {
                Users.Add(userVM);
                Cancel();
            }
        }
        async void ChangeUser(UserVM user)
        {
            user.SurName = SurName;
            user.Name = Name;
            user.Login = Login;
            user.Password = Password;
            user.Mail = Mail;
            user.AccessLevel = AccessLevel;
            user.Notes = Notes;

            if (await _userRepository.ChangeUser(user)) Cancel();
        }
        void Cancel()
        {
            Visibility = false;
            ClearProperties();
        }
        void ClearProperties()
        {
            SurName = string.Empty;
            Name = string.Empty;
            Login = string.Empty;
            Password = string.Empty;
            Mail = string.Empty;
            AccessLevel = string.Empty;
            Notes = string.Empty;
        }
    }
}
