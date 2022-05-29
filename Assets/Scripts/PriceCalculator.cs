using System.Collections.Generic;

public class PriceCalculator: IPriceCalculator
{
    private readonly List<ILocation> _sellingLocations = new List<ILocation>();
    private readonly List<IProduct> _products = new List<IProduct>();
    private readonly IVehicle _vehicle;

    public PriceCalculator(IVehicle vehicle)
    {
        _vehicle = vehicle;
    }

    public void AddSellingLocation(ILocation location)
    {
        AddExistingProductsToNewLocation(location);
        _sellingLocations.Add(location);
    }

    private void AddExistingProductsToNewLocation(ILocation location)
    {
        foreach (IProduct product in _products)
        {
            location.AddProduct(product);
        }
    }

    public int GetSellingLocationsQuantity()
    {
        return _sellingLocations.Count;
    }

    public void AddProduct(IProduct product)
    {
        _products.Add(product);
        AddNewProductToExistingLocations(product);
    }

    public ILocation GetBestLocationToSell()
    {
        ILocation bestLocation = new Location(0, "There is no good place to sell.");
        int bestLocationPrice = 0;
        foreach (ILocation location in _sellingLocations)
        {
            int locationPrice = CalculateLocationPrice(location);
            if (locationPrice <= bestLocationPrice) continue;
            bestLocation = location;
            bestLocationPrice = locationPrice;
        }
        return bestLocation;
    }

    public void AddProductToVehicle(IProduct product, int weight)
    {
        _vehicle.AddProduct(product, weight);
    }

    public int GetVanProductsQuantity()
    {
        return _vehicle.GetProductsQuantity();
    }

    public void RemoveLocation(ILocation location)
    {
        _sellingLocations.Remove(location);
    }

    public void RemoveProduct(IProduct product)
    {
        RemoveProductFromProductsList(product);
        RemoveProductFromExistingLocations(product);
    }

    private void RemoveProductFromProductsList(IProduct product)
    {
        _products.Remove(product);
    }

    private void RemoveProductFromExistingLocations(IProduct product)
    {
        foreach (ILocation sellingLocation in _sellingLocations)
        {
            sellingLocation.RemoveProduct(product);
        }
    }

    private int CalculateLocationPrice(ILocation location)
    {
        int locationPrice = 0;
        
        foreach (KeyValuePair<IProduct, int> productKeyValue in _vehicle.GetProductsAndQuantity())
        {
            IProduct product = productKeyValue.Key;
            int quantity = productKeyValue.Value;
            locationPrice += location.GetProductPrice(product) * quantity;
        }

        return locationPrice;
    }

    private void AddNewProductToExistingLocations(IProduct product)
    {
        foreach (ILocation location in _sellingLocations)
        {
            location.AddProduct(product);
        }
    }

    public int GetProductsQuantity()
    {
        return _products.Count;
    }
}