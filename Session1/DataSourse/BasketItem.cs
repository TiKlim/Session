using System;

namespace Session1.DataSourse;

public class BasketItem
{
    private int BIId; //Айдишник товара в корзине
    private int BIPId; //Айдишник товара из списка товаров, который добавлен в корзину (связь между товаром из списка и товаром в корзине)
    private string BSName; //Название товара в корзине
    private double BDPrice; //Цена товара в корзине
    private int BICount; //количество товара в корзине
        
    public BasketItem(int iId, int iPId, string sName, double dPrice, int iCount)
    {
        BIId = iId;
        BIPId = iPId;
        BSName = sName;
        BDPrice = dPrice;
        BICount = iCount;
    }

    public int BIid
    {
        get { return BIId; }
        set { BIId = value; }
    }

    public int BIPid
    {
        get { return BIPId; }
        set { BIPId = value; }
    }

    public string BSname
    {
        get { return BSName; }
        set { BSName = value; }
    }

    public double BDprice
    {
        get { return BDPrice; }
        set { BDPrice = Math.Round(value,2); }
    }

    public int BIcount
    {
        get { return BICount; }
        set { BICount = value; }
    }
}