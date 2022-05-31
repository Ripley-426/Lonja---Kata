public interface IPriceCalculator
{
    void AddCity(ICity city);
    int GetCitiesQuantity();
    int GetCurrentFishQuantity();
    void AddFish(IFish fishName);
    Top3Cities GetBestCityToSell();
    bool ChangeFishQuantityInVehicle(IFish fish, int weight);
    int GetVehicleProductsQuantity();
    void RemoveCity(ICity cityName);
    void RemoveFish(IFish fish);
    void ChangeVehicleCapacity(int weight);
    void ChangeCityDistance(ICity city, int distance);
    void ChangeFishPrice(ICity city, IFish fish, int newPrice);
}