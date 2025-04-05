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

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void MinimizedButton_Click(object sender, RoutedEventArgs e)
    {
        this.WindowState = WindowState.Minimized;
    }

    private void ToggleButton_Click(object sender, RoutedEventArgs e)
    {
        int minWidth = 1150;
        int minHeight = 900;

        if (this.WindowState == WindowState.Maximized)
        {
            this.WindowState = WindowState.Normal;
            this.Width = minWidth;
            this.Height = minHeight;
        }
        else
        {
            this.WindowState = WindowState.Maximized;
        }
    }
}
