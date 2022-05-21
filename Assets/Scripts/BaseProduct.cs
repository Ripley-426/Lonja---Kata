public class BaseProduct : IProduct
{
    private string _name;

    public BaseProduct(string name)
    {
        _name = name;
    }
}