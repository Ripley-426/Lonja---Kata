public interface ICity
{
    void AddFish(IFish fish);
    int GetDistance();
    string GetName();
    int GetProductsQuantity();
    int GetFishPrice(IFish fish);
    void SetProductPrice(IFish fish, int price);
    void RemoveFish(IFish fish);
    void SetDistance(int distance);
}