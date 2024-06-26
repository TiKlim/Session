using Avalonia.Controls;
using Avalonia.Interactivity;
using Session1.Windows;

namespace Session1;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Enter.Click += EnterOnClick;
        Guest.Click += GuestOnClick;
    }

    private void GuestOnClick(object? sender, RoutedEventArgs e)
    {
        Authorized atrd = new Authorized();
        atrd.Show();
        Close();
    }

    private void EnterOnClick(object? sender, RoutedEventArgs e)
    {
        Authorized atrd = new Authorized();
        atrd.Show();
        Close();
    }
}