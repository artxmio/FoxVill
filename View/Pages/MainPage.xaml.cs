using FoxVill.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace FoxVill.View.Pages;

public partial class MainPage : Page
{
    private readonly MainWindowViewModel _viewModel;

    public MainPage(MainWindowViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;

        this.DataContext = _viewModel;
    }

    private void AddItemToLiked_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            button.Tag = button.Tag?.ToString() == "0" ? "1" : "0";
        }
    }
}
