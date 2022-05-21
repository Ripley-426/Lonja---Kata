using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace Tests
{
    public class PriceCalculatorShould
    {
        private IPriceCalculator _priceCalculator;
        private ILocation _location;
        private ILocation _secondLocation;
        private IProduct _product;
        private IProduct _secondProduct;
        private IVehicle _vehicle;

        [SetUp]
        public void Setup()
        {
            _vehicle = Substitute.For<IVehicle>();
            _priceCalculator = new PriceCalculator(_vehicle);
            _location = Substitute.For<ILocation>();
            _secondLocation = Substitute.For<ILocation>();
            _product = Substitute.For<IProduct>();
            _secondProduct = Substitute.For<IProduct>();
        }
        
        [Test]
        public void BeAbleToAddSellingLocations()
        {
            _priceCalculator.AddSellingLocation(_location);
            _priceCalculator.AddSellingLocation(_secondLocation);
            
            Assert.AreEqual(2, _priceCalculator.GetSellingLocationsQuantity());
        }

        [Test]
        public void BeAbleToRemoveSellingLocations()
        {
            _priceCalculator.AddSellingLocation(_secondLocation);
            _priceCalculator.AddSellingLocation(_location);
            _priceCalculator.RemoveLocation(_location);
            
            Assert.AreEqual(1, _priceCalculator.GetSellingLocationsQuantity());
        }

        [Test]
        public void BeAbleToAddProducts()
        {
            _priceCalculator.AddProduct(_product);
            _priceCalculator.AddProduct(_secondProduct);
            
            Assert.AreEqual(2, _priceCalculator.GetProductsQuantity());
        }
        
        [Test]
        public void BeAbleToRemoveProducts()
        {
          _priceCalculator.AddProduct(_secondProduct);
          _priceCalculator.AddProduct(_product);

          _priceCalculator.RemoveProduct(_product);
          
          Assert.AreEqual(1, _priceCalculator.GetProductsQuantity());
        }

        [Test]
        public void RemoveProductFromLocationsWhenRemovingProductFromCalculator()
        {
            _priceCalculator.AddSellingLocation(_location);
            _priceCalculator.AddSellingLocation(_secondLocation);
            _priceCalculator.AddProduct(_secondProduct);
            _priceCalculator.AddProduct(_product);
            _priceCalculator.RemoveProduct(_product);

            _location.Received(1).RemoveProduct(_product);
            _secondLocation.Received(1).RemoveProduct(_product);
        }
        
        [Test]
        public void BeAbleToAddProductsToTheVehicle()
        {
            _priceCalculator.AddProductToVehicle(_product, 50);
            _priceCalculator.AddProductToVehicle(_secondProduct, 50);
            
            _vehicle.ReceivedWithAnyArgs(2).AddProduct(default, 50);
        }

        [Test]
        public void AddANewProductToExistingLocations()
        {
            _priceCalculator.AddSellingLocation(_location);
            _priceCalculator.AddSellingLocation(_secondLocation);
            
            _priceCalculator.AddProduct(_product);

            _location.Received(1).AddProduct(_product);
            _secondLocation.Received(1).AddProduct(_product);
        }

        [Test]
        public void AddExistingProductsToNewLocation()
        {
            _priceCalculator.AddProduct(_product);
            _priceCalculator.AddSellingLocation(_location);

            _location.Received(1).AddProduct(_product);
        }

        [Test]
        public void CalculateBestSellingLocation()
        {
            _location.GetProductPrice(_product).Returns(100);
            _location.GetProductPrice(_secondProduct).Returns(200);
            _secondLocation.GetProductPrice(_product).Returns(80);
            _secondLocation.GetProductPrice(_secondProduct).Returns(160);
            _location.GetName().Returns("Madrid");

            Dictionary<IProduct, int> vehicleProducts = new Dictionary<IProduct, int>
            {
                { _product, 50 },
                { _secondProduct, 60 }
            };

            _vehicle.GetProductsAndQuantity().Returns(vehicleProducts);


            _priceCalculator.AddProduct(_product);
            _priceCalculator.AddProduct(_secondProduct);
            _priceCalculator.AddSellingLocation(_location);
            _priceCalculator.AddSellingLocation(_secondLocation);
            _priceCalculator.AddProductToVehicle(_product, 20);
            _priceCalculator.AddProductToVehicle(_secondProduct, 30);
            
            Assert.AreEqual(_location.GetName(), _priceCalculator.GetBestLocationToSell().GetName());
        }
    }
}