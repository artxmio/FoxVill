using FoxVill.View.Animation;
using FoxVill.ViewModel;
using System.Windows;

namespace FoxVill.View;

public partial class UserDataWindow : Window
{
    private ProfileViewModel _viewModel;

    public UserDataWindow(ProfileViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;

        this.DataContext = _viewModel;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        AnimationManager.CloseWithFade(this);
    }
}
