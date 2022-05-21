using System.Collections.Generic;

public interface IVehicle
{
    int GetProductsQuantity();
    void AddProduct(IProduct product, int weight);
    List<IProduct> GetProducts();
    Dictionary<IProduct, int> GetProductsAndQuantity();
}