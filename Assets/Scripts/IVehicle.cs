using System.Collections.Generic;

public interface IVehicle
{
    int GetProductsQuantity();
    void AddFish(IFish fish);
    List<IFish> GetFish();
    Dictionary<IFish, int> GetProductsAndQuantity();
    bool ChangeFishWeight(IFish fishName, int weight);
    void SetCapacity(int weight);
}