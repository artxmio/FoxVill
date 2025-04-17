using FoxVill.View.Animation;
using FoxVill.View.Pages;
using FoxVill.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FoxVill.View;

public partial class MainWindow : Window
{
    private readonly MainWindowViewModel _viewModel;

    public MainWindow(MainWindowViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;

        this.DataContext = _viewModel;

        this.MainFrame.Navigate(new MainPage(_viewModel));
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
        if (Mouse.DirectlyOver is TextBox ||
            Mouse.DirectlyOver is TextBlock ||
             Mouse.DirectlyOver is CheckBox
            || Mouse.DirectlyOver is Button)
        {
            return;
        }

        const int minWidth = 1600;
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
