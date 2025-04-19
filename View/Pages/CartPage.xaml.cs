using FoxVill.View.Animation;
using FoxVill.ViewModel;
using System.Windows;
using System.Windows.Controls;

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

        listView.ItemsSource = new List<object>() { new() };
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        var mainPage = new MainPage(_viewModel, _window);
        AnimationManager.NavigateWithAnimation(_window.MainFrame, mainPage);
    }
}
