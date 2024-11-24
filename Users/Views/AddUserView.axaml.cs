using Avalonia.Controls;
using Users.Common.IoC;

namespace Users;

public partial class AddUserView : Window, IView
{
    public AddUserView() => InitializeComponent();

    public void Exit() => Close();
}