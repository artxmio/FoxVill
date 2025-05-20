using FoxVill.View.Animation;
using FoxVill.View.Pages;
using FoxVill.ViewModel;
using Microsoft.EntityFrameworkCore;
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

        this.MainFrame.Navigate(new MainPage(_viewModel, this));
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.A) && Keyboard.IsKeyDown(Key.D))
        {
            _viewModel.OpenAdminWindowCommand.Execute(this);
        }

        if (e.Key == Key.Escape)
        {
            var result = MessageBox.Show("Вы действительно хотите закрыть окно?", "Подтверждение",
                                         MessageBoxButton.YesNo, MessageBoxImage.Asterisk);

            if (result == MessageBoxResult.Yes)
            {
                this.Close(); 
            }
        }
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
            Mouse.DirectlyOver is CheckBox ||
            Mouse.DirectlyOver is Button ||
            Mouse.DirectlyOver is ListViewItem)
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
