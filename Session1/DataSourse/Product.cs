using Avalonia.Media.Imaging;

namespace Session1.DataSourse;

public class Product
{
    private string sNameProduct;
    private double dPriceProduct;
    private int iIdProduct;
    private int iCountInBasket;
    private string sSupplierProduct;
    private int iMeasurement;
    private string sDescription;
    private string? SProductImageSource; //Источник картинки, может быть не указан. Включает в себя только название файла с расширением
    private int ICathegoryId; //Идентификатор категории
    
    public Product(string sName, double dPrice, int id, int iCount, string sSupplier, int iMeasure, string sDes, string sImg, int iCathegory)
    {
        sNameProduct = sName;
        dPriceProduct = dPrice;
        iIdProduct = id;
        iCountInBasket = iCount;
        sSupplierProduct = sSupplier;
        iMeasurement = iMeasure;
        sDescription = sDes;
        SProductImageSource = sImg;
        ICathegoryId = iCathegory;
    }
    public string Name
    {
        get { return sNameProduct;}
        set { sNameProduct = value; }
    }
    public double Price
    {
        get { return dPriceProduct; }
        set { dPriceProduct = value; }
    }

    public int Idd
    {
        get { return iIdProduct; }
        set { iIdProduct = value; }
    }
    
    public int Count
    {
        get { return iCountInBasket; }
        set { iCountInBasket = value; }
    }
    
    public string Supplier
    {
        get { return sSupplierProduct; }
        set { sSupplierProduct = value; }
    }
    
    public int Measurement
    {
        get { return iMeasurement; }
        set { iMeasurement = value; }
    }
    
    public string Description
    {
        get { return sDescription; }
        set { sDescription = value; }
    }
    
    public string? ImageSource
    {
        get { return SProductImageSource; }
        set { SProductImageSource = value; }
    }
    
    public int Cathegory
    {
        get { return ICathegoryId; }
        set { ICathegoryId = value; }
    }
    
    public Bitmap? BitmapImage => ImageSource != null ? new Bitmap($"Assets/{ImageSource}") : new Bitmap("Assets/placeholder.jpg"); //Если источник не указан, используется заглушка
}