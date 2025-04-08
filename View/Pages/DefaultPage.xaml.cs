using FoxVill.View.Animation;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Windows.Controls;

namespace FoxVill.View.Pages;

public partial class DefaultPage : Page
{
    private readonly AuthorizationWindow _window;

    public DefaultPage(AuthorizationWindow window)
    {
        InitializeComponent();
        _window = window;
    }

    private void OpenAuthWindow(object sender, System.Windows.RoutedEventArgs e)
    {
        var authPage = new AuthPage(_window);

        AnimationManager.NavigateWithAnimation(_window.MainFrame, authPage);
    }

    private void OpenRegistrationWindow(object sender, System.Windows.RoutedEventArgs e)
    {
        var regPage = new RegPage(_window);

        AnimationManager.NavigateWithAnimation(_window.MainFrame, regPage);
    }
}
