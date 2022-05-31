using System.Collections.Generic;

public class PriceCalculator: IPriceCalculator
{
    private readonly List<ICity> _cities = new List<ICity>();
    private readonly List<IFish> _currentFish = new List<IFish>();
    private readonly IVehicle _vehicle;

    public PriceCalculator(IVehicle vehicle)
    {
        _vehicle = vehicle;
    }

    public void AddCity(ICity city)
    {
        AddExistingFishToNewCity(city);
        _cities.Add(city);
    }

    private void AddExistingFishToNewCity(ICity city)
    {
        foreach (IFish fish in _currentFish)
        {
            city.AddFish(fish);
        }
    }

    public int GetCitiesQuantity()
    {
        return _cities.Count;
    }

    public void AddFish(IFish fish)
    {
        _currentFish.Add(fish);
        _vehicle.AddFish(fish);
        AddNewFishToExistingCities(fish);
    }

    public ICity GetBestCityToSell()
    {
        ICity bestCity = new City("There is no good city to sell.");
        int bestCityPrice = 0;
        foreach (ICity city in _cities)
        {
            int cityPrice = CalculateCityPrice(city);
            if (cityPrice <= bestCityPrice) continue;
            bestCity = city;
            bestCityPrice = cityPrice;
        }
        return bestCity;
    }

    public bool ChangeFishQuantityInVehicle(IFish fish, int weight)
    {
        return _vehicle.ChangeFishWeight(fish, weight);
    }

    public int GetVehicleProductsQuantity()
    {
        return _vehicle.GetProductsQuantity();
    }

    public void RemoveCity(ICity city)
    {
        _cities.Remove(city);
    }

    public void RemoveFish(IFish fishName)
    {
        RemoveFishFromCurrentFish(fishName);
        RemoveFishFromExistingCities(fishName);
    }

    public void ChangeVehicleCapacity(int weight)
    {
        _vehicle.SetCapacity(weight);
    }

    public void ChangeCityDistance(ICity cityToChange, int distance)
    {
        _cities.Find(city => city.GetName() == cityToChange.GetName()).SetDistance(distance);
    }

    public void ChangeFishPrice(ICity cityToChange, IFish fishToChange, int newPrice)
    {
        _cities.Find(city => city.GetName() == cityToChange.GetName()).SetProductPrice(fishToChange, newPrice);
    }

    private void RemoveFishFromCurrentFish(IFish fish)
    {
        _currentFish.Remove(fish);
    }

    private void RemoveFishFromExistingCities(IFish fish)
    {
        foreach (ICity city in _cities)
        {
            city.RemoveFish(fish);
        }
    }

    private int CalculateCityPrice(ICity city)
    {
        int cityPrice = 0;
        
        foreach (KeyValuePair<IFish, int> fishKeyValue in _vehicle.GetProductsAndQuantity())
        {
            IFish fish = fishKeyValue.Key;
            int quantity = fishKeyValue.Value;
            cityPrice += city.GetFishPrice(fish) * quantity;
        }

        return cityPrice;
    }

    private void AddNewFishToExistingCities(IFish fishName)
    {
        foreach (ICity city in _cities)
        {
            city.AddFish(fishName);
        }
    }

    public int GetCurrentFishQuantity()
    {
        return _currentFish.Count;
    }
}