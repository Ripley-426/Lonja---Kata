using System.Collections.Generic;
using System.Linq;

public class Vehicle: IVehicle
{
    private int _totalWeight;
    private const int Capacity = 200;
    private readonly Dictionary<IFish, int> _currentFish = new Dictionary<IFish, int>();
    
    public int GetCapacity()
    {
        return Capacity;
    }

    public int GetWeight()
    {
        return _totalWeight;
    }

    public bool LoadProduct(IFish fish, int weight)
    {
        if (_totalWeight + weight <= Capacity)
        {
            _totalWeight += weight;
            AddFish(fish);
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetProductsQuantity()
    {
        return _currentFish.Count;
    }

    public void AddFish(IFish fish)
    {
        _currentFish.Add(fish, 0);
    }

    public List<IFish> GetFish()
    {
        return _currentFish.Keys.ToList();
    }

    public Dictionary<IFish, int> GetProductsAndQuantity()
    {
        return _currentFish;
    }

    public bool ChangeFishWeight(IFish fish, int weight)
    {
        if (_totalWeight + weight - _currentFish[fish] > Capacity) return false;
        _totalWeight += weight;
        _currentFish[fish] = weight;
        return true;
    }
}