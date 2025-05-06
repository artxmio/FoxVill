using FoxVill.View.Animation;
using FoxVill.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FoxVill.View.Pages;

public partial class CartPage : Page
{
    private readonly MainWindowViewModel _viewModel;
    private readonly MainWindow _window;

    public CartPage(MainWindowViewModel viewModel, MainWindow window)
    {
        InitializeComponent();

        _viewModel = viewModel;
        _window = window;

        this.DataContext = _viewModel;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        var mainPage = new MainPage(_viewModel, _window);
        AnimationManager.NavigateWithAnimation(_window.MainFrame, mainPage);
    }

    private void btn1_Click(object sender, RoutedEventArgs e)
    {
        btn2.Background = Brushes.Gray;
        btn1.Background = Brushes.White;
    }

    private void btn2_Click(object sender, RoutedEventArgs e)
    {
        btn1.Background = Brushes.Gray;
        btn2.Background = Brushes.White;
    }
}
