using System;
using System.Collections.Generic;
using System.Linq;

public class City: ICity
{
    private readonly string _name;
    private int _distance;
    private readonly Dictionary<IFish, int> _fishPrices = new Dictionary<IFish, int>();

    public City(string name)
    {
        _name = name;
    }
    public void AddFish(IFish fish)
    {
        _fishPrices.Add(fish, 0);
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
        return _fishPrices.Count;
    }

    public int GetFishPrice(IFish fish)
    {
        return CalculateDistanceValue(_fishPrices[fish]);
    }

    private int CalculateDistanceValue(int fishPrice)
    {
        return CalculatePercentageOfLostPrice() > 100 ? 0 : ReducePriceByPercentage(fishPrice, CalculatePercentageOfLostPrice());
    }

    private int ReducePriceByPercentage(int productPrice, int percentageOfLostPrice)
    {
        return productPrice - (productPrice * percentageOfLostPrice / 100);
    }

    private int CalculatePercentageOfLostPrice()
    {
        return _distance / 100;
    }

    public void SetProductPrice(IFish fish, int price)
    {
        _fishPrices[fish] = price;
    }

    public void RemoveFish(IFish fishName)
    {
        _fishPrices.Remove(fishName);
    }

    public void SetDistance(int distance)
    {
        _distance = distance;
    }
}