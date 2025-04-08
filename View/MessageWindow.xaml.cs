using System.Windows;

namespace FoxVill.View;

public partial class MessageWindow 
{
    public MessageWindow(string message)
    {
        InitializeComponent();

        textBlock.Text = message;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
