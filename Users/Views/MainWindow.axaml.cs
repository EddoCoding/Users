using Avalonia.Controls;
using Users.Common.IoC;

namespace Users.Views
{
    public partial class MainWindow : Window, IView
    {
        public MainWindow() => InitializeComponent();

        public void Exit() => Close();
    }
}