using Avalonia;
using Avalonia.Markup.Xaml;
using Users.Common;
using Users.Common.IoC;
using Users.ViewModels;
using Users.Views;

namespace Users
{
    public partial class App : Application
    {
        IContainer _container = new Container();
        IServiceView _serviceView;

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);

            _serviceView = _container.GetDependency<IServiceView>();

            _container.RegisterTransient<UserRepository, IUserRepository>();
            _container.RegisterTransient<DataContext, DataContext>();

            _serviceView.RegisterTypeView<MainWindowViewModel, MainWindow>();
            _serviceView.RegisterTypeView<AddUserViewModel, AddUserView>();
            _serviceView.RegisterTypeView<ChangeUserViewModel, ChangeUserView>();

            _serviceView.Window<MainWindowViewModel>().NonModal();
        }
    }
}