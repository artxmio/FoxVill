﻿using FoxVill.View.Animation;
using FoxVill.ViewModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WpfAnimatedGif;

namespace FoxVill.View.Pages;

public partial class ProfilePage : Page
{
    private readonly MainWindow _window;
    private readonly ProfileViewModel _viewModel;

    public ProfilePage(ProfileViewModel viewModel, MainWindow window)
    {
        InitializeComponent();

        _window = window;
        _viewModel = viewModel;

        this.DataContext = _viewModel;

        LoadGif();
    }

    private void LoadGif()
    {
        var image = new BitmapImage(new Uri("../../Resources/Images/Gif/fox.gif", UriKind.Relative));
        ImageBehavior.SetAnimatedSource(gifImage, image);
    }

    private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        var mainPage = new MainPage((MainWindowViewModel)_window.DataContext, _window);

        AnimationManager.NavigateWithAnimation(_window.MainFrame, mainPage);
    }

    private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
    {
        var cartPage = new CartPage((MainWindowViewModel)_window.DataContext, _window);

        AnimationManager.NavigateWithAnimation(_window.MainFrame, cartPage);
    }

    private void Button_Click_2(object sender, System.Windows.RoutedEventArgs e)
    {
        var mainPage = new MainPage((MainWindowViewModel)_window.DataContext, _window);

        AnimationManager.NavigateWithAnimation(_window.MainFrame, mainPage);

        ((MainWindowViewModel)_window.DataContext).ShowFavoritesCommand.Execute(_window.MainFrame);
    }

    private void Button_Click_3(object sender, System.Windows.RoutedEventArgs e)
    {
        var dataWindow = new UserDataWindow(_viewModel);

        AnimationManager.ShowModalWindow(_window, dataWindow);
    }

    private void Button_Click_4(object sender, RoutedEventArgs e)
    {
        var window = new PaymentMethodsWindow(_viewModel);

        AnimationManager.ShowModalWindow(_window, window);
    }

    private void Button_Click_5(object sender, RoutedEventArgs e)
    {
        string email = "example@mail.com";
        string subject = "Тема письма";
        string body = "Добрый день!\n\nНапишите ваш текст здесь.";

        string mailtoLink = $"mailto:{email}?subject={subject}&body={body}";

        Process.Start(new ProcessStartInfo(mailtoLink) { UseShellExecute = true });
    }

    private void Button_Click_6(object sender, RoutedEventArgs e)
    {
        try
        {
            string url = @"Resources\Html\faq.html"; 
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
        catch(Exception ex) 
        {
            MessageBox.Show($"{ex.Message}", "Ошибка");
        }

    }

    private void Button_Click_7(object sender, RoutedEventArgs e)
    { 
        var page = new HistoryPage(_viewModel, _window);

        AnimationManager.NavigateWithAnimation(_window.MainFrame, page);
    }
}
