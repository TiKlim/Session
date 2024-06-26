using System.Collections.Generic;

namespace Session1.DataSourse;

public class DataBase
{
    public List<Product> Products = new List<Product>()
    {
        new Product("Булка хлеба", 39.90, "type", 0, 20, "Поставщик", "шт", "Вкусный, сдобный, зашибись"),
        new Product("Молоко", 69.99, "type", 1, 20, "Поставщик", "шт", "Вкусное, полезное, зашибись"),
        new Product("Сыр", 130.90, "type", 2, 0, "Поставщик", "шт", "Вкусный, сырный, зашибись"),
        new Product("Кефир", 68.99, "type", 3, 0, "Поставщик", "шт", "Вкусный, кислый, зашибись"),
        new Product("Соль", 88.90, "type", 4, 5, "Поставщик", "шт", "Вкусная, балдёжная, зашибись"),
        new Product("Сахар", 88.90, "type", 5, 0, "Поставщик", "шт", "Вкусный, сладкий, зашибись")
    };

    public Users UAuthorizedUser = null; //Авторизованный пользователь

    public List<Users> UUsers = new List<Users>()
    {
        new Users("Admin", "Admin", true, false),
        new Users("User", "User", false, false)

    };
}