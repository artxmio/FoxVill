using System.Windows.Media.Animation;
using System.Windows;
using System.Windows.Controls;

namespace FoxVill.View.Animation;

public static class AnimationManager
{
    public static void ShowModalWindow(Window ownerWindow, Window modalWindow)
    {
        if (ownerWindow is null || modalWindow is null)
        {
            throw new NullReferenceException("ownerWindow or modalWindow can't be null!");
        }

        modalWindow.Opacity = 0;
        modalWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        modalWindow.Owner = ownerWindow;

        var fadeIn = new DoubleAnimation
        {
            From = 0.0,
            To = 1.0,
            Duration = TimeSpan.FromSeconds(0.5)
        };

        modalWindow.Loaded += (s, e) =>
        {
            modalWindow.BeginAnimation(Window.OpacityProperty, fadeIn);
        };

        modalWindow.ShowDialog();
    }

    public static void CloseWithFade(Window modalWindow)
    {
        if (modalWindow is null)
        {
            throw new NullReferenceException("modalWindow can't be null!");
        }

        var fadeOut = new DoubleAnimation
        {
            From = 1.0, 
            To = 0.0,   
            Duration = TimeSpan.FromSeconds(0.5)
        };

        fadeOut.Completed += (s, e) =>
        {
            modalWindow.Close(); 
        };

        modalWindow.BeginAnimation(Window.OpacityProperty, fadeOut);
    }

    public static void NavigateWithAnimation(Frame frame,Page nextPage)
    {
        var fadeOut = new DoubleAnimation
        {
            From = 1.0,
            To = 0.0,
            Duration = TimeSpan.FromSeconds(0.5)
        };

        fadeOut.Completed += (s, e) =>
        {
            frame.Navigate(nextPage);

            var fadeIn = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = TimeSpan.FromSeconds(0.5)
            };

            frame.BeginAnimation(Frame.OpacityProperty, fadeIn);
        };

        frame.BeginAnimation(Frame.OpacityProperty, fadeOut);
    }

    public static void CloseMainWindow(Window window)
    {
        if (window is null)
        {
            throw new NullReferenceException("MainWindow cann't be null!");
        }

        var fadeOut = new DoubleAnimation
        {
            From = 1.0,
            To = 0.7,
            Duration = TimeSpan.FromSeconds(0.1)
        };

        fadeOut.Completed += (s, e) =>
        {
            window.Close();
        };

        window.BeginAnimation(Window.OpacityProperty, fadeOut);
    }
}
