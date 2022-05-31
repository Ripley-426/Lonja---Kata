public interface IPriceCalculator
{
    void AddCity(ICity city);
    int GetCitiesQuantity();
    int GetCurrentFishQuantity();
    void AddFish(IFish fishName);
    ICity GetBestCityToSell();
    bool ChangeFishQuantityInVehicle(IFish fish, int weight);
    int GetVehicleProductsQuantity();
    void RemoveCity(ICity cityName);
    void RemoveFish(IFish fish);
    void ChangeVehicleCapacity(int weight);
}