using FoxVill.View.Animation;
using System.Windows;
using System.Windows.Controls;

namespace FoxVill.View.Pages;

public partial class RegPage : Page
{
    private readonly AuthorizationWindow _window;

    public RegPage(AuthorizationWindow window)
    {
        InitializeComponent();

        _window = window;
    }

    private void AboutClick(object sender, RoutedEventArgs e)
    {
        var modalWindow = new AboutWindow();

        AnimationManager.ShowModalWindow(_window, modalWindow);
    }

    private void TextBlock_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        AnimationManager.NavigateWithAnimation(_window.MainFrame, new AuthPage(_window));
    }
}
