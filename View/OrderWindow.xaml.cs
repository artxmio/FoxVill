using FoxVill.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace FoxVill.View;

public partial class OrderWindow : Window
{
    private readonly OrderWindowViewModel _viewModel;

    public OrderWindow(OrderWindowViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;

        this.DataContext = viewModel;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        if (PaymentMethodComboBox.SelectedItem is not null && CvcTextBox.Text.Length == 3)
        {
            DialogResult = true;
            this.Close();
        }
        else
        {
            DialogResult = false;
            this.Close();
        }
    }
    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Escape)
        {
            this.Close();
        }
    }
    private void CvcTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^\d+$");
    }
}
