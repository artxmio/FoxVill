using FoxVill.ViewModel;
using System.Windows;
using System.Windows.Input;

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

    private void CardNumberBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^\d+$");
    }
}
