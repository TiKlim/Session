using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Session1.DataSourse;


namespace Session1.Windows;

public partial class Basket : Window
{
    private double startPrice = 0; 
    public Basket()
    {
        InitializeComponent();
        BasketList.ItemsSource = Helper.DataObj.UAuthorizedUser.LUserBasket.ToArray();
        SetPrice();
    }
    private void SetPrice()
    {
        for (int i = 0; i < Helper.DataObj.UAuthorizedUser.LUserBasket.Count; i++) 
        {
            startPrice += Helper.DataObj.UAuthorizedUser.LUserBasket[i].BDprice; 
        }
        startPrice = Helper.DataObj.UAuthorizedUser.LUserBasket.Count > 0 ? startPrice : 0;
        itemPrice.Text = Convert.ToString(startPrice);
    }

    private void CartActivity(object? sender, Avalonia.Interactivity.RoutedEventArgs e) 
    {
        var button = (sender as Button)!;
        switch (button.Name)
        {
            case "Return": //возвращение к списку товаров
                {
                    Authorized Authorized = new();
                    Authorized.Show();
                    Close();
                }
                break;
            case "Clear": //Очистка всей корзины
                {
                    for (int i = 0; i < Helper.DataObj.Products.Count; i++)
                    {
                        for (int j = 0; j < Helper.DataObj.UAuthorizedUser.LUserBasket.Count; j++) 
                        {
                            if (Helper.DataObj.Products[i].Idd == Helper.DataObj.UAuthorizedUser.LUserBasket[j].BIPid) 
                            {
                                Helper.DataObj.Products[i].Count += Helper.DataObj.UAuthorizedUser.LUserBasket[j].BIcount; 
                            }
                        }
                    }
                    Helper.DataObj.UAuthorizedUser.LUserBasket.Clear(); 
                    SetPrice(); 
                    BasketList.ItemsSource = Helper.DataObj.UAuthorizedUser.LUserBasket.ToArray();
                }
                break;
            case "Delete":
                {
                    for (int i = 0; i < Helper.DataObj.Products.Count; i++)  
                    {
                        if (Helper.DataObj.Products[i].Idd == Helper.DataObj.UAuthorizedUser.LUserBasket[(int)button!.Tag!].BIPid)
                        {
                            Helper.DataObj.Products[i].Count += Helper.DataObj.UAuthorizedUser.LUserBasket[(int)button!.Tag!].BIcount;
                        }
                    }
                    Helper.DataObj.UAuthorizedUser.LUserBasket.RemoveAt((int)button!.Tag!);
                    for (int j = 0; j < Helper.DataObj.UAuthorizedUser.LUserBasket.Count; j++)
                    {
                        Helper.DataObj.UAuthorizedUser.LUserBasket[j].BIid = j;
                    }
                    BasketList.ItemsSource = Helper.DataObj.UAuthorizedUser.LUserBasket.ToArray();
                    SetPrice();
                }
                break;
        }
    }
}