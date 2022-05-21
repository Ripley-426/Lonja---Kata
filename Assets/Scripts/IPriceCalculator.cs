public interface IPriceCalculator
{
    void AddSellingLocation(ILocation location);
    int GetSellingLocationsQuantity();
    int GetProductsQuantity();
    void AddProduct(IProduct product);
    ILocation GetBestLocationToSell();
    void AddProductToVehicle(IProduct product, int weight);
    int GetVanProductsQuantity();
    void RemoveLocation(ILocation location);
    void RemoveProduct(IProduct product);
}