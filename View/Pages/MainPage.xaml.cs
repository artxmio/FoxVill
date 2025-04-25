using FoxVill.View.Animation;
using FoxVill.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace FoxVill.View.Pages;

public partial class MainPage : Page
{
    private readonly MainWindowViewModel _viewModel;
    private readonly MainWindow _window;

    public MainPage(MainWindowViewModel viewModel, MainWindow window)
    {
        InitializeComponent();

        _viewModel = viewModel;
        _window = window;

        this.DataContext = _viewModel;
    }

    private void AddItemToLiked_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            button.Tag = button.Tag?.ToString() == "0" ? "1" : "0";
        }
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        var profilePage = new ProfilePage(_viewModel, _window);

        AnimationManager.NavigateWithAnimation(_window.MainFrame, profilePage);
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        var cartPage = new CartPage(_viewModel, _window);

        AnimationManager.NavigateWithAnimation(_window.MainFrame, cartPage);
    }

    private void Button_Click_2(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.ContextMenu != null)
        {
            button.ContextMenu.IsOpen = true;
        }
    }

    private void SortByDescendingClick(object sender, System.Windows.RoutedEventArgs e)
    {
        _viewModel.SortByDescendingCommand.Execute(null);
    }
    private void SortByAscendingClick(object sender, System.Windows.RoutedEventArgs e)
    {
        _viewModel.SortByAscendingCommand.Execute(null);
    }
    private void SortByTitleClick(object sender, System.Windows.RoutedEventArgs e)
    {
        _viewModel.SortByTitleCommand.Execute(null);
    }
    private void SortByTitleRevertClick(object sender, System.Windows.RoutedEventArgs e)
    {
        _viewModel.SortByTitleRevertCommand.Execute(null);
    }
}
