using FoxVill.ViewModel;
using System.Windows.Controls;

namespace FoxVill.View.Pages;

public partial class HistoryPage : Page
{
    public HistoryPage(ProfileViewModel viewModel)
    {
        InitializeComponent();

        this.DataContext = viewModel;
    }
}
