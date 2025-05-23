﻿using FoxVill.Model;
using FoxVill.View.Animation;
using FoxVill.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace FoxVill.View.Pages;

public partial class MainPage : Page
{
    private readonly MainWindowViewModel _viewModel;
    private readonly MainWindow _window;

    public MainPage(MainWindowViewModel viewModel, MainWindow window)
    {
        InitializeComponent();

        _viewModel = viewModel;
        _window = window;

        this.DataContext = _viewModel;
    }

    private void AddItemToLiked_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            button.Tag = button.Tag?.ToString() == "0" ? "1" : "0";
        }
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        var profileViewModel = new ProfileViewModel(new DataBase.DatabaseContext(), _viewModel.CurrentUser);
        var profilePage = new ProfilePage(profileViewModel, _window);

        AnimationManager.NavigateWithAnimation(_window.MainFrame, profilePage);
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        var cartPage = new CartPage(_viewModel, _window);

        AnimationManager.NavigateWithAnimation(_window.MainFrame, cartPage);
    }

    private void Button_Click_2(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.ContextMenu != null)
        {
            button.ContextMenu.IsOpen = true;
        }
    }

    private void SortByDescendingClick(object sender, System.Windows.RoutedEventArgs e)
    {
        _viewModel.SortByDescendingCommand.Execute(null);
    }
    private void SortByAscendingClick(object sender, System.Windows.RoutedEventArgs e)
    {
        _viewModel.SortByAscendingCommand.Execute(null);
    }
    private void SortByTitleClick(object sender, System.Windows.RoutedEventArgs e)
    {
        _viewModel.SortByTitleCommand.Execute(null);
    }
    private void SortByTitleRevertClick(object sender, System.Windows.RoutedEventArgs e)
    {
        _viewModel.SortByTitleRevertCommand.Execute(null);
    }

    private void SearchBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (e.Key == System.Windows.Input.Key.Enter)
        {
            _viewModel.SearchString = searchbox.Text;
        }
    }

    private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        var product = (sender as Border)?.DataContext as Product;

        if (product is not null)
        {
            _viewModel.OpenProductCardCommand.Execute(product);
        }
    }
}
