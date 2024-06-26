using System.Collections.Generic;

namespace Session1.DataSourse;

public class DataBase
{
    public List<Product> Products = new List<Product>()
    {
        new Product("Булка хлеба", 39.90, "type", 0, 20, "Поставщик", 6, "Вкусный, сдобный, зашибись")
    };
}