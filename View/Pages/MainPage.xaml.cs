using System.Windows;
using System.Windows.Controls;

namespace FoxVill.View.Pages;

public partial class MainPage : Page
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void AddItemToLiked_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            button.Tag = button.Tag?.ToString() == "0" ? "1" : "0";
        }
    }
}
