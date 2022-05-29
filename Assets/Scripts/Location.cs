using System;
using System.Collections.Generic;

public class Location: ILocation
{
    private readonly int _distance;
    private readonly string _name;
    private readonly Dictionary<IProduct, int> _productPrices = new Dictionary<IProduct, int>();

    public Location(int distance, string name)
    {
        _distance = distance;
        _name = name;
    }
    public void AddProduct(IProduct product)
    {
        _productPrices.Add(product, 0);
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
        return _productPrices.Count;
    }

    public int GetProductPrice(IProduct product)
    {
        return CalculateDistanceValue(_productPrices[product]);
    }

    private int CalculateDistanceValue(int productPrice)
    {
        return CalculatePercentageOfLostPrice() > 100 ? 0 : ReducePriceByPercentage(productPrice, CalculatePercentageOfLostPrice());
    }

    private int ReducePriceByPercentage(int productPrice, int percentageOfLostPrice)
    {
        return productPrice - (productPrice * percentageOfLostPrice / 100);
    }

    private int CalculatePercentageOfLostPrice()
    {
        return _distance / 100;
    }

    public void SetProductPrice(IProduct product, int price)
    {
        _productPrices[product] = price;
    }

    public void RemoveProduct(IProduct product)
    {
        throw new System.NotImplementedException();
    }
}