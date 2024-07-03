using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Session1.DataSourse;


namespace Session1.Windows;

public partial class Authorized : Window
{
    private List<string> LSuppliers = []; //список поставщиков
    private string[] SPrice = ["По умолчанию", "По возрастанию", "По убыванию"];

    public Authorized()
    {
        InitializeComponent();
        addProduct.IsVisible = Helper.DataObj.UAuthorizedUser.UAdmin;
        toShopping.IsVisible = !Helper.DataObj.UAuthorizedUser.UGuest;
        LBoxInitialization(Helper.DataObj.Products);

        tblock_user.Text += Helper.DataObj.UAuthorizedUser.UAdmin == false ? (Helper.DataObj.UAuthorizedUser.UGuest == true ? "Гость" : "Юзер") : "Админ"; 
        CartCountVisibility();

        SupplierUpdate();
        search.Text = Helper.DataObj.SSearchBar; 
        ComboBoxInit(); 
    }

    private void LBoxInitialization(List<Product> listBoxSource) 
    {
        ListOfProducts.ItemsSource = listBoxSource.Select(x => new
        {
            x.Name,
            x.Idd,
            x.Description,
            x.Supplier,
            x.ImageSource,
            x.Price,
            x.Count,
            MeasuremenT = " " + Helper.DataObj.Measurem[x.Measurement],
            Admin = Helper.DataObj.UAuthorizedUser.UAdmin,
            Guest = Helper.DataObj.UAuthorizedUser.UGuest, 
            Color = x.Count > 0 ? "White" : "Gray" //Для товаров с количеством 0
        });
    }

    private void SupplierUpdate()
    {
        LSuppliers.Add("Все поставщики");
        foreach (Product product in Helper.DataObj.Products)
        {
            if (!LSuppliers.Contains(product.Supplier))
                LSuppliers.Add(product.Supplier);
        }
    }

    private void ComboBoxInit()
    {
        sortSuppliers.ItemsSource = LSuppliers;
        sortSuppliers.SelectedIndex = Helper.DataObj.ISelectedSupplier;
        sortPrice.ItemsSource = SPrice; 
        sortPrice.SelectedIndex = Helper.DataObj.ISelectedPrice;
    }

    private List<BasketItem> UsersCartCheck() 
    {
        foreach (Users Users in Helper.DataObj.UUsers) //Перебор всех пользователей
        {
            if (Users.LUserBasket.Count > 0) 
                return Users.LUserBasket; 
        }
        return null; 
    }
    

    private void OpenRedWindow()
    {
        AddItem AddItem = new();
        AddItem.Show();
        Close();
    }

    private void LogOut(object? sender, Avalonia.Interactivity.RoutedEventArgs e) 
    {
        Helper.DataObj.UAuthorizedUser = null; //Очистка статического поля "авторизированный пользователь"
        Helper.DataObj.SSearchBar = "";
        Helper.DataObj.ISelectedPrice = Helper.DataObj.ISelectedSupplier = 0;
        MainWindow mainWindow = new();
        mainWindow.Show();
        Close();
    }

    private void ToCart(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Basket Basket = new();
        Basket.Show();
        Close();
    }
    

    private void ProductManipulation(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (UsersCartCheck() == null) 
        {
            var button = (sender as Button)!;
            switch (button.Name)
            {
                case "addProduct": 
                    {
                        OpenRedWindow();
                    }
                    break;
                case "cost": 
                    {
                        Helper.DataObj.SelectedProduct = Helper.DataObj.Products[(int)button!.Tag!];
                        OpenRedWindow();
                    }
                    break;
                case "del": 
                    {
                        if (Helper.DataObj.Products[(int)button!.Tag!].ImageSource != null) //Если у удаляемого товара есть картинка, то картинка удаляется вместе с товаром
                        {
                            string imgDel = Helper.DataObj.Products[(int)button!.Tag!].ImageSource; 
                            Helper.DataObj.Products[(int)button!.Tag!].ImageSource = null; 
                            File.Delete($"Assets/{imgDel}"); 
                        }
                        Helper.DataObj.Products.RemoveAt((int)button!.Tag!); 
                        for (int i = 0; i < Helper.DataObj.Products.Count; i++) 
                        {
                            Helper.DataObj.Products[i].Idd = i;
                        }
                        SearchingAndSorting(); //сортировка
                    }
                    break;
            }
        }
    }

    private void SearchingActivity(object? sender, Avalonia.Input.KeyEventArgs e) //Поиск товаров по имени
    {
        Helper.DataObj.SSearchBar = search.Text; 
        SearchingAndSorting();
    }

    private void SelectionChanging(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
    {
        var combobox = (sender as ComboBox)!;
        switch (combobox.Name)
        {
            case "sortSuppliers": //Поставщики
                Helper.DataObj.ISelectedSupplier = combobox.SelectedIndex;
                break;
            case "sortPrice": //Цена
                Helper.DataObj.ISelectedPrice = combobox.SelectedIndex;
                break;
        }
        SearchingAndSorting(); //Поиск
    }

    private void SuppliersSorting() //Выборка по поставщику
    {
        Helper.DataObj.FoundedProducts.Clear(); //Очистка найденных товаров
        if (sortSuppliers.SelectedIndex != 0 && sortSuppliers.SelectedIndex != -1) //Если выбранный индекс не равен 0 и не равен -1
        {
            foreach (Product product in Helper.DataObj.Products)
            {
                if (product.Supplier == LSuppliers[sortSuppliers.SelectedIndex])
                {
                    Helper.DataObj.FoundedProducts.Add(product);
                }
            }
        }
        LBoxInitialization(sortSuppliers.SelectedIndex == 0 ? Helper.DataObj.Products : Helper.DataObj.FoundedProducts);
    }

    private void Searching()
    {
        List<Product> searching = []; 
        searching.AddRange(sortSuppliers.SelectedIndex == 0 ? Helper.DataObj.Products : Helper.DataObj.FoundedProducts);
        if (search.Text != "") 
        {
            Helper.DataObj.FoundedProducts.Clear(); 
            string[] keywords = search.Text.Split(';');
            foreach (Product product in searching) 
            {
                if (product.Name.Trim().ToLower().Contains(keywords[0].Trim().ToLower())) 
                {
                    if (keywords.Length == 1) 
                    {
                        Helper.DataObj.FoundedProducts.Add(product); 
                    }
                    else if (product.Description.Trim().ToLower().Contains(keywords[1].Trim().ToLower())) 
                    {
                        if (keywords.Length == 2) 
                        {
                            Helper.DataObj.FoundedProducts.Add(product);
                        }
                        else if (product.Supplier.Trim().ToLower().Contains(keywords[2].Trim().ToLower()))
                        {
                            if (keywords.Length == 3) 
                            {
                                Helper.DataObj.FoundedProducts.Add(product); 
                            }
                            else if (Convert.ToString(product.Price).Trim().ToLower().Contains(keywords[3].Trim().ToLower())) 
                            {
                                if (keywords.Length == 4) 
                                {
                                    Helper.DataObj.FoundedProducts.Add(product); 
                                }
                                else if (Convert.ToString(product.Count).Trim().ToLower().Contains(keywords[4].Trim().ToLower())) 
                                {
                                    if (keywords.Length == 5) 
                                    {
                                        Helper.DataObj.FoundedProducts.Add(product); 
                                    }
                                    else if (Helper.DataObj.Measurem[product.Measurement].Trim().ToLower().Contains(keywords[5].Trim().ToLower()))
                                    {
                                        if (keywords.Length == 6) 
                                            Helper.DataObj.FoundedProducts.Add(product);//Добавление товара в список найденных товаров
                                    }
                                }
                            }
                        }
                    }
                }
            }
            LBoxInitialization(Helper.DataObj.FoundedProducts);
        }
        else
        {
            LBoxInitialization(searching);
        }
    }

    private void FoundInfo() 
    {
        if (sortSuppliers.SelectedIndex != 0 || search.Text != "") 
        {
            searchCount.Text = $"{Helper.DataObj.FoundedProducts.Count} из {Helper.DataObj.Products.Count}"; 
            searchCount.IsVisible = true; 
        }
        else
        {
            searchCount.IsVisible = false;
        }
    }

    private void SearchingAndSorting() //Поиск
    {
        SuppliersSorting(); 
        Searching(); 
        BubbleSorting(sortPrice.SelectedIndex); 
        FoundInfo(); 
    }

    private void BubbleSorting(int selectedOption) //Сортировка пузырьком
    {
        List<Product> bubble = []; 
        bubble.AddRange(sortSuppliers.SelectedIndex != 0 || search.Text != "" ? Helper.DataObj.FoundedProducts : Helper.DataObj.Products); 
        switch (selectedOption)
        {
            case 1: //По возрастанию
                {
                    for (int i = 0; i < bubble.Count; i++)
                        for (int j = 0; j < bubble.Count - i - 1; j++)
                        {
                            if (bubble[j].Price > bubble[j + 1].Price)
                            {
                                Product temp = bubble[j];
                                bubble[j] = bubble[j + 1];
                                bubble[j + 1] = temp;
                            }
                        }
                }
                break;
            case 2: //По убыванию
                {
                    for (int i = 0; i < bubble.Count; i++)
                        for (int j = 0; j < bubble.Count - i - 1; j++)
                        {
                            if (bubble[j].Price < bubble[j + 1].Price)
                            {
                                Product temp = bubble[j];
                                bubble[j] = bubble[j + 1];
                                bubble[j + 1] = temp;
                            }
                        }
                }
                break;
        }
        LBoxInitialization(bubble);
    }

    private void CartCountVisibility() 
    {
        cartCount.Text = "Позиций в корзине: " + Convert.ToString(Helper.DataObj.UAuthorizedUser.LUserBasket.Count); 
        cartCount.IsVisible = Helper.DataObj.UAuthorizedUser.LUserBasket.Count > 0 ? true : false; //Установка видимости этого текста в зависимости от наличия позиций в корзине
    }

    private void AddToCart(Product product) 
    {
        for (int i = 0; i < Helper.DataObj.UAuthorizedUser.LUserBasket.Count; i++) 
        {
            if (Helper.DataObj.UAuthorizedUser.LUserBasket[i].BIPid == product.Idd) 
            {
                Helper.DataObj.UAuthorizedUser.LUserBasket[i].BIcount++;
                product.Count--; 
                Helper.DataObj.UAuthorizedUser.LUserBasket[i].BDprice = Helper.DataObj.UAuthorizedUser.LUserBasket[i].BDprice + product.Price; 
                return; 
            }
        }
        Helper.DataObj.UAuthorizedUser.LUserBasket.Add(new BasketItem(Helper.DataObj.UAuthorizedUser.LUserBasket.Count, product.Idd, product.Name, product.Price, 1)); //Создается новый товар (для корзины) и добавляется в корзину
        product.Count--; 
    }

    private void RemoveFromCart(Product product)
    {
        for (int i = 0; i < Helper.DataObj.UAuthorizedUser.LUserBasket.Count; i++) //Перебор элементов корзины
        {
            if (Helper.DataObj.UAuthorizedUser.LUserBasket[i].BIPid == product.Idd) 
            {
                Helper.DataObj.UAuthorizedUser.LUserBasket[i].BIcount--; 
                Helper.DataObj.UAuthorizedUser.LUserBasket[i].BDprice = Helper.DataObj.UAuthorizedUser.LUserBasket[i].BDprice - product.Price; 
                product.Count++; 
                if (Helper.DataObj.UAuthorizedUser.LUserBasket[i].BIcount == 0) 
                {
                    Helper.DataObj.UAuthorizedUser.LUserBasket.RemoveAt(i); 
                    for (int j = 0; j < Helper.DataObj.UAuthorizedUser.LUserBasket.Count; j++) 
                    {
                        Helper.DataObj.UAuthorizedUser.LUserBasket[j].BIid = j;
                    }
                }
                return;
            }
        }
    }

    private void CartManipulation(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var button = (sender as Button)!;
        switch (button.Name)
        {
            case "cartAdd":
                {
                    if (Helper.DataObj.Products[(int)button!.Tag!].Count > 0)
                    {
                        AddToCart(Helper.DataObj.Products[(int)button!.Tag!]);
                    }
                }
                break;
            case "cartDel":
                {
                    if (Helper.DataObj.UAuthorizedUser.LUserBasket.Count > 0) //Если корзина не пуста
                    {
                        RemoveFromCart(Helper.DataObj.Products[(int)button!.Tag!]);
                    }
                }
                break;
        }
        SearchingAndSorting(); //Поиск
        CartCountVisibility(); //Отображение количества
    }
}