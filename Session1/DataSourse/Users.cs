namespace Session1.DataSourse;

public class Users
{
    private string sLoginUser; //логин
    private string sPasswordUser; //пароль
    private bool bIfTheAdmin; //булево админ
    private bool bIfTheGuest; //булево гость
    //private List<Product> lUserProduct = [];

    public Users(string login, string password, bool admin, bool guest)
    {
        sLoginUser = login;
        sPasswordUser = password;
        bIfTheAdmin = admin;
        bIfTheGuest = guest;
    }

    public string Login
    {
        get { return sLoginUser; }
        set { sLoginUser = value; }
    }

    public string Password
    {
        get { return sPasswordUser; }
        set { sPasswordUser = value; }
    }

    public bool Admin
    {
        get { return bIfTheAdmin; }
        set { bIfTheAdmin = value; }
    }

    public bool Guest
    {
        get { return bIfTheGuest; }
        set { bIfTheGuest = value; }
    }

    /*public List<Product> UserProduct
    {
        get { return _UserCart; }
        set { _UserCart = value; }
    }*/
}