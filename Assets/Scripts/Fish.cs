public class Fish : IFish
{
    private readonly string _name;

    public Fish(string name)
    {
        _name = name;
    }

    public string GetName()
    {
        return _name;
    }
}