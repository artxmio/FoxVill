using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FoxVill.View.Pages;

public partial class AuthPage : Page
{
    private readonly AuthorizationWindow _window;

    public AuthPage(AuthorizationWindow window)
    {
        InitializeComponent();
        _window = window;
    }

    
}
