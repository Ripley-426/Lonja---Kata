﻿using System.Collections.Generic;
using System.Linq;

public class Vehicle: IVehicle
{
    private int _totalWeight;
    private int _capacity = 200;
    private Dictionary<IProduct, int> _products = new Dictionary<IProduct, int>();
    public int GetCapacity()
    {
        return _capacity;
    }

    public int GetWeight()
    {
        return _totalWeight;
    }

    public bool LoadProduct(IProduct product, int weight)
    {
        if (_totalWeight + weight <= _capacity)
        {
            _totalWeight += weight;
            AddProduct(product, weight);
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetProductsQuantity()
    {
        return _products.Count;
    }

    public void AddProduct(IProduct product, int weight)
    {
        _products.Add(product, weight);
    }

    public List<IProduct> GetProducts()
    {
        return _products.Keys.ToList();
    }

    public Dictionary<IProduct, int> GetProductsAndQuantity()
    {
        return _products;
    }
}