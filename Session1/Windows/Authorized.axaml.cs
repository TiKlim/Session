using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;
using Session1.DataSourse;

namespace Session1.Windows;

public partial class Authorized : Window
{
    public Authorized()
    {
        InitializeComponent();
        SetData();
        
    }
    
    private void SetData()
    {
        ListOfProducts.ItemsSource = Helper.DataObj.Products.OrderBy(x => x.Idd).Select(x => new
        {
            x.Name, x.Price, x.Type, x.Idd, x.Count, x.Supplier, x.Measurement, x.Description,
            Color = x.Count > 0 ? "White" : "Gray" //У товаров с количетсвом 0 будет серый фон
        });
    }
}