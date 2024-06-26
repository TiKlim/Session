using Avalonia.Controls;
using Avalonia.Interactivity;
using Session1.DataSourse;
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
        Authorized auto = new Authorized();
        auto.Show();
        Close();
    }

    private void EnterOnClick(object? sender, RoutedEventArgs e)
    {

        var button = (sender as Button)!;
        switch (button.Name)
        {
            case "Enter": //Кнопка логин
            {
                FalseLogin.IsVisible = false;
                FalsePass.IsVisible = false;
                if (Login.Text != null || Login.Text != "" || Password.Text != null || Password.Text != "") //Если в поля логина и пароля хоть что-то введено
                {
                    foreach (Users user in Helper.DataObj.UUsers)
                    {
                        if (user.ULogin == Login.Text) //Если логин совпадает с именем одного из пользователей
                        {
                            FalseLogin.IsVisible = false; //Скрытие сообщения о неверном логине
                            if (user.UPassword == Password.Text) //Если введенный пароль совпадает с паролем найденного пользователя
                            {
                                Helper.DataObj.UAuthorizedUser = user; //Авторизированный пользователь становится найденным пользователем
                                Authorized authorized = new(); //Переход к окну списка товаров
                                authorized.Show();
                                Close();
                            }
                            else
                            {
                                FalsePass.IsVisible = true; //Появление уведомления о неверном пароле
                                break;
                            }
                        }
                        else
                        {
                            FalseLogin.IsVisible = true; //Появление уведомлений о неверном логине...
                            FalsePass.IsVisible = true; //и пароле
                        }
                    }
                }
            }
                break;
            case "Guest":
            {
                Helper.DataObj.UAuthorizedUser = new("guest", "", false, true); //Авторизированным пользователем становится новый объект класса User с параметрами гостя
                GuestOnClick(sender, e);
            }
                break;
        }
    }
}
