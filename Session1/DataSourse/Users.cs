using System.Collections.Generic;

namespace Session1.DataSourse;

public class Users
{
    private string sLoginUser; //логин
    private string sPasswordUser; //пароль
    private bool bIfTheAdmin; //булево админ
    private bool bIfTheGuest; //булево гость
    private List<BasketItem> UserBasket = [];
    //private List<Product> lUserProduct = [];

    public Users(string login, string password, bool admin, bool guest)
    {
        sLoginUser = login;
        sPasswordUser = password;
        bIfTheAdmin = admin;
        bIfTheGuest = guest;
    }

    public string ULogin
    {
        get { return sLoginUser; }
        set { sLoginUser = value; }
    }

    public string UPassword
    {
        get { return sPasswordUser; }
        set { sPasswordUser = value; }
    }

    public bool UAdmin
    {
        get { return bIfTheAdmin; }
        set { bIfTheAdmin = value; }
    }

    public bool UGuest
    {
        get { return bIfTheGuest; }
        set { bIfTheGuest = value; }
    }

    public List<BasketItem> LUserBasket
    {
        get { return UserBasket; }
        set { UserBasket = value; }
    }
}