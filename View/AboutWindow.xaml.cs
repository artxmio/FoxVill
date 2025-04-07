using FoxVill.View.Animation;
using System.Windows;

namespace FoxVill.View;

public partial class AboutWindow : Window
{
    public AboutWindow()
    {
        InitializeComponent();
    }

    private void CloseButtonClick(object sender, RoutedEventArgs e)
    {
        AnimationManager.CloseWithFade(this);
    }
}
