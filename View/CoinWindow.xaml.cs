using NAudio.Wave;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FoxVill.View;

public partial class CoinWindow : Window
{
    private bool isDragging = false;
    private Point mouseOffset;
    private UIElement selectedElement;

    private WaveOutEvent _outputDevice = null!;

    private bool success1;
    private bool success2;
    private bool success3;
    private bool success4;
    private bool success5;

    public CoinWindow()
    {
        InitializeComponent();

        Loaded += CoinWindow_Loaded;
        Closed += CoinWindow_Closed;
    }

    private void CoinWindow_Loaded(object sender, RoutedEventArgs e)
    {
        var audioFile = new AudioFileReader("Resources/Music/mp3/forest_sound.mp3");

        _outputDevice = new WaveOutEvent();
        _outputDevice.Init(audioFile);
        _outputDevice.Volume = 0.1f;
        _outputDevice.Play();
    }

    private void CoinWindow_Closed(object? sender, EventArgs e)
    {
        _outputDevice.Stop();
    }
    private void Image_MouseDown(object sender, MouseButtonEventArgs e)
    {
        isDragging = true;
        selectedElement = sender as UIElement;
        mouseOffset = e.GetPosition(selectedElement);
        selectedElement.CaptureMouse();
    }

    private void Image_MouseMove(object sender, MouseEventArgs e)
    {
        if (isDragging && selectedElement != null)
        {
            Point position = e.GetPosition(this);
            Canvas.SetLeft(selectedElement, position.X - mouseOffset.X);
            Canvas.SetTop(selectedElement, position.Y - mouseOffset.Y);
        }
    }

    private void Image_MouseUp(object sender, MouseButtonEventArgs e)
    {
        isDragging = false;
        selectedElement?.ReleaseMouseCapture();

        if (selectedElement != null)
        {
            CheckCoinPlacement(selectedElement);
        }

        selectedElement = null;
    }

    private void CheckCoinPlacement(UIElement coin)
    {
        Rect chest1Zone = new Rect(Canvas.GetLeft(chest1), Canvas.GetTop(chest1), chest1.Width, chest1.Height);
        Rect chest2Zone = new Rect(Canvas.GetLeft(chest2), Canvas.GetTop(chest2), chest2.Width, chest2.Height);
        Rect chest3Zone = new Rect(Canvas.GetLeft(chest3), Canvas.GetTop(chest2), chest2.Width, chest2.Height);
        Rect chest4Zone = new Rect(Canvas.GetLeft(chest4), Canvas.GetTop(chest2), chest2.Width, chest2.Height);
        Rect chest5Zone = new Rect(Canvas.GetLeft(chest5), Canvas.GetTop(chest2), chest2.Width, chest2.Height);

        Rect coinRect = new Rect(Canvas.GetLeft(coin), Canvas.GetTop(coin), (coin as Image).Width, (coin as Image).Height);

        if (coin == FoxMoneyGreen && chest1Zone.IntersectsWith(coinRect))
        {
            success1 = true;
            coin.Visibility = Visibility.Collapsed;
            chest1.Source = new BitmapImage(new Uri("/Resources/Images/Png/close_chest.png", UriKind.Relative));
        }
        else if (coin == FoxMoneyIce && chest2Zone.IntersectsWith(coinRect))
        {
            success2 = true;
            coin.Visibility = Visibility.Collapsed;
            chest2.Source = new BitmapImage(new Uri("/Resources/Images/Png/close_chest.png", UriKind.Relative));
        }
        else if (coin == FoxMoneyYellow && chest3Zone.IntersectsWith(coinRect))
        {
            success3 = true;
            coin.Visibility = Visibility.Collapsed;
            chest3.Source = new BitmapImage(new Uri("/Resources/Images/Png/close_chest.png", UriKind.Relative));
        }
        else if (coin == FoxMoneyPickme && chest4Zone.IntersectsWith(coinRect))
        {
            success4 = true;
            coin.Visibility = Visibility.Collapsed;
            chest4.Source = new BitmapImage(new Uri("/Resources/Images/Png/close_chest.png", UriKind.Relative));
        }
        else if (coin == FoxMoneyForest && chest5Zone.IntersectsWith(coinRect))
        {
            success5 = true;
            coin.Visibility = Visibility.Collapsed;
            chest5.Source = new BitmapImage(new Uri("/Resources/Images/Png/close_chest.png", UriKind.Relative));
        }

        if (success1 && success2 && success3 && success4 && success5)
        {
            MessageBox.Show("Успех, сделай скриншот этого окна,\n назови свой адрес электронной почты \nи получи скидку 5% на наличную оплату в кассах нашего парка!");
            SaveWinResult();
            this.Close();
        }
    }
    private void SaveWinResult()
    {
        File.WriteAllText("win_data.txt", "Win");
    }
}
