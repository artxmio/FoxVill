using FoxVill.View.Animation;
using FoxVill.ViewModel;
using System.Windows.Controls;

namespace FoxVill.View.Pages;

public partial class ProfilePage : Page
{
    private readonly MainWindow _window;
    private readonly MainWindowViewModel _viewModel;

    public ProfilePage(MainWindowViewModel viewModel, MainWindow window)
    {
        InitializeComponent();

        _window = window;
        _viewModel = viewModel;

        this.DataContext = _viewModel;
    }

    private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        var mainPage = new MainPage(_viewModel, _window);

        AnimationManager.NavigateWithAnimation(_window.MainFrame, mainPage);
    }
}
