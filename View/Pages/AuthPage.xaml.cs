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

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        this._window.Close();
    }

    private void MinimizedButton_Click(object sender, RoutedEventArgs e)
    {
        this._window.WindowState = WindowState.Minimized;
    }

    private void ToggleButton_Click(object sender, RoutedEventArgs e)
    {
        if (this._window.WindowState == WindowState.Maximized)
        {
            this._window.WindowState = WindowState.Normal;
            this._window.Width = 1150;
            this._window.Height = 800;
        }
        else
        {
            this._window.WindowState = WindowState.Maximized;
        }
    }
}
