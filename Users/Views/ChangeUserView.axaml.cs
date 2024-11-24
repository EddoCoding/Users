using Avalonia.Controls;
using Users.Common.IoC;

namespace Users;

public partial class ChangeUserView : Window, IView
{
    public ChangeUserView() => InitializeComponent();

    public void Exit() => Close();
}