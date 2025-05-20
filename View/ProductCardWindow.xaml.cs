using FoxVill.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace FoxVill.View;

public partial class ProductCardWindow : Window
{
    private MainWindowViewModel _mainViewModel;
    private readonly ProductCardWindowViewModel _viewModel;

    public ProductCardWindow(ProductCardWindowViewModel viewModel, MainWindowViewModel mainViewModel)
    {
        InitializeComponent();
        this._mainViewModel = mainViewModel;
        this._viewModel = viewModel;
        this.DataContext = viewModel;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Escape)
        {
            this.Close();
        }
    }
    private void AddItemToLiked_Click(object sender, RoutedEventArgs e)
    {
        _mainViewModel.ChangeProductFavoriteStateCommand.Execute(this);
    }
}
