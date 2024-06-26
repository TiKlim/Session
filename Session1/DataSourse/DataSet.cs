using System.Collections.Generic;

namespace Session1.DataSourse;

public class DataBase
{
    public List<Product> Products = new List<Product>()
    {
        new Product("Булка хлеба", 39.90, "type", 0, 20, "Поставщик", "шт", "Вкусный, сдобный, зашибись"),
        new Product("Молоко", 69.99, "type", 1, 20, "Поставщик", "шт", "Вкусное, полезное, зашибись"),
        new Product("Сыр", 130.90, "type", 2, 0, "Поставщик", "шт", "Вкусный, сырный, зашибись")
    };

    public Users UAuthorizedUser = null; //Авторизованный пользователь

    public List<Users> UUsers = new List<Users>()
    {
        new Users("Admin", "Admin", true, false),
        new Users("User", "User", false, false)

    };
}