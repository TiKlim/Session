using System.Collections.Generic;

namespace Session1.DataSourse;

public class DataBase
{
    public List<Product> Products = new List<Product>()
    {
        new Product("Булка хлеба", 39.90, 0, 20, "Поставщик", 0, "Вкусный, сдобный, зашибись", null, 1),
        new Product("Молоко", 69.99, 1, 20, "Поставщик", 2, "Вкусное, полезное, зашибись", null, 1),
        new Product("Сыр", 130.90, 2, 0, "Поставщик", 5, "Вкусный, сырный, зашибись", null, 1),
        new Product("Кефир", 68.99, 3, 0, "Поставщик", 2, "Вкусный, кислый, зашибись", null, 1),
        new Product("Соль", 88.90, 4, 5, "Поставщик", 5, "Вкусная, балдёжная, зашибись", null, 1),
        new Product("Сахар", 88.90, 5, 0, "Поставщик", 5, "Вкусный, сладкий, зашибись", null, 1),
        new Product("Вода", 47.90, 6, 8, "Поставщик", 2, "Вкусная, жидкая, зашибись", null, 1)
    };

    public Users UAuthorizedUser = null; //Авторизованный пользователь
    public Product PSelectedProduct = null; //Выбранный для редактирования товар
    public Product SelectedProduct = null;

    public List<Users> UUsers = new List<Users>()
    {
        new Users("Admin", "Admin", true, false),
        new Users("User", "User", false, false)

    };
    public string SSearchBar = ""; //Поисковая строка
    public int ISelectedPrice = 0; //Индекс элемента выпадающего списка цены
    public int ISelectedSupplier = 0; //Индекс элемента выпадающего списка производителей
    public List<string> Measurem = ["шт", "кг", "л", "км", "м", "г"]; //единицы измерения
    public List<string> Cathegories = ["Общее", "Продукты питания", "Техника", "Одежда"]; //категории товаров
    public List<Product> FoundedProducts = []; //Список найденнных товаров
}