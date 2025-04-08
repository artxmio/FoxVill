using FoxVill.View.Animation;
using FoxVill.View.Pages;
using FoxVill.ViewModel;
using System.Windows;
using System.Windows.Controls;
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

        var authPage = new DefaultPage(this);
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
        AnimationManager.CloseMainWindow(this);
    }

    private void MinimizedButton_Click(object sender, RoutedEventArgs e)
    {
        this.WindowState = WindowState.Minimized;
    }

    private void ToggleButton_Click(object sender, RoutedEventArgs e)
    {
        if (Mouse.DirectlyOver is TextBox||
            Mouse.DirectlyOver is TextBlock ||
             Mouse.DirectlyOver is CheckBox
            || Mouse.DirectlyOver is Button)
        {
            return;
        }

        const int minWidth = 1150;
        const int minHeight = 900;

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
