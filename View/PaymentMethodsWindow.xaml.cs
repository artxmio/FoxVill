using FoxVill.ViewModel;
using System.Windows;

namespace FoxVill.View;

public partial class PaymentMethodsWindow : Window
{
    private readonly ProfileViewModel _viewModel;

    public PaymentMethodsWindow(ProfileViewModel viewModel)
    {
        InitializeComponent();
        this._viewModel = viewModel;

        DataContext = _viewModel;
    }
}
