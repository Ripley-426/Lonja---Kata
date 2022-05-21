using System.Collections.Generic;

public class Location: ILocation
{
    private readonly int _distance;
    private readonly string _name;
    private readonly Dictionary<IProduct, int> _products = new Dictionary<IProduct, int>();

    public Location(int distance, string name)
    {
        _distance = distance;
        _name = name;
    }
    public void AddProduct(IProduct product)
    {
        _products.Add(product, 0);
    }

    public int GetDistance()
    {
        return _distance;
    }

    public string GetName()
    {
        return _name;
    }

    public int GetProductsQuantity()
    {
        return _products.Count;
    }

    public int GetProductPrice(IProduct product)
    {
        return _products[product];
    }

    public void SetProductPrice(IProduct product, int price)
    {
        _products[product] = price;
    }

    public void RemoveProduct(IProduct product)
    {
        throw new System.NotImplementedException();
    }
}