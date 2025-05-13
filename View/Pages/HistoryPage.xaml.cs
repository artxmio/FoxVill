using FoxVill.View.Animation;
using FoxVill.ViewModel;
using System.Windows.Controls;

namespace FoxVill.View.Pages;

public partial class HistoryPage : Page
{
    private readonly ProfileViewModel _viewModel;
    private readonly MainWindow _window;

    public HistoryPage(ProfileViewModel viewModel, MainWindow window)
    {
        InitializeComponent();

        this._viewModel = viewModel;
        this._window = window;

        this.DataContext = viewModel;
    }

    private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        var page = new ProfilePage(_viewModel, _window);

        AnimationManager.NavigateWithAnimation(_window.MainFrame, page);
    }
}
