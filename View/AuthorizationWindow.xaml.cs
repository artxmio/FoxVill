using FoxVill.View.Pages;
using FoxVill.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace FoxVill.View;

public partial class AuthorizationWindow : Window
{
    private readonly AuthorizationWindowViewModel _viewModel;

    public AuthorizationWindow()
    {
        InitializeComponent();

        _viewModel = new AuthorizationWindowViewModel();

        this.DataContext = _viewModel;

        var authPage = new AuthPage(this);
        MainFrame.Navigate(authPage);
    }

    private void DragModeWindow(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (e.ButtonState == MouseButtonState.Pressed)
        {
            this.DragMove();
        }
    }
}
