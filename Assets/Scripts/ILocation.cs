public interface ILocation
{
    void AddProduct(IProduct product);
    int GetDistance();
    string GetName();
    int GetProductsQuantity();
    int GetProductPrice(IProduct product);
    void SetProductPrice(IProduct product, int price);
    void RemoveProduct(IProduct product);
}