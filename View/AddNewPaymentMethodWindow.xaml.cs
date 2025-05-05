using FoxVill.ViewModel;
using System.Windows;

namespace FoxVill.View;

public partial class AddNewPaymentMethodWindow : Window
{
    public AddNewPaymentMethodWindow(AddNewPaymentMethodViewModel viewModel)
    {
        InitializeComponent();

        this.DataContext = viewModel;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
