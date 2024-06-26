namespace Session1.DataSourse;

public class Product
{
    private string sNameProduct;
    private double dPriceProduct;
    private string sTypeProduct;
    private int iIdProduct;
    private int iCountInBasket;
    private string sSupplierProduct;
    private string sMeasurement;
    private string sDescription;
    
    public Product(string sName, double dPrice, string sType, int id, int iCount, string sSupplier, string sMeasure, string sDes)
    {
        sNameProduct = sName;
        dPriceProduct = dPrice;
        sTypeProduct = sType;
        iIdProduct = id;
        iCountInBasket = iCount;
        sSupplierProduct = sSupplier;
        sMeasurement = sMeasure;
        sDescription = sDes;
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
    public string Type
    {
        get { return sTypeProduct; }
        set { sTypeProduct = value; }
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
    
    public string Measurement
    {
        get { return sMeasurement; }
        set { sMeasurement = value; }
    }
    
    public string Description
    {
        get { return sDescription; }
        set { sDescription = value; }
    }
    
}