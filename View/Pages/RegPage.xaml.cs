﻿using FoxVill.View.Animation;
using System.Windows;
using System.Windows.Controls;

namespace FoxVill.View.Pages;

public partial class RegPage : Page
{
    private readonly AuthorizationWindow _window;

    public RegPage(AuthorizationWindow window)
    {
        InitializeComponent();

        _window = window;

        DataContext = _window.DataContext;
    }

    private void AboutClick(object sender, RoutedEventArgs e)
    {
        var modalWindow = new AboutWindow();

        AnimationManager.ShowModalWindow(_window, modalWindow);
    }

    private void TextBlock_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        AnimationManager.NavigateWithAnimation(_window.MainFrame, new AuthPage(_window));
    }

    private void ChangeRepeatPasswordVisible(object sender, RoutedEventArgs e)
    {
        repeatRegistrationBox.IsPasswordVisible = !repeatRegistrationBox.IsPasswordVisible;

        if (sender is Button button)
        {
            button.Tag = button.Tag?.ToString() == "0" ? "1" : "0";
        }
    }   
    
    private void ChangeMainPasswordVisible(object sender, RoutedEventArgs e)
    {
        mainRegistrationBox.IsPasswordVisible = !mainRegistrationBox.IsPasswordVisible;

        if (sender is Button button)
        {
            button.Tag = button.Tag?.ToString() == "0" ? "1" : "0";
        }
    }

    private void ClosePage(object sender, RoutedEventArgs e)
    {
        var defaultPage = new DefaultPage(_window);

        AnimationManager.NavigateWithAnimation(_window.MainFrame, defaultPage);
    }
}
