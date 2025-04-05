using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FoxVill.View.Pages;

public partial class AuthPage : Page
{
    private readonly AuthorizationWindow _window;

    public AuthPage(AuthorizationWindow window)
    {
        InitializeComponent();
        _window = window;
    }

    private void MinimizedButton_Click(object sender, RoutedEventArgs e)
    {
        var window = new AboutWindow();

        window.ShowDialog();
    }
}
