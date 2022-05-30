using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace Tests
{
    public class PriceCalculatorShould
    {
        private IPriceCalculator _priceCalculator;
        private ICity _city;
        private ICity _secondCity;
        private IFish _fish;
        private IFish _secondFish;
        private IVehicle _vehicle;

        [SetUp]
        public void Setup()
        {
            _vehicle = Substitute.For<IVehicle>();
            _priceCalculator = new PriceCalculator(_vehicle);
            _city = Substitute.For<ICity>();
            _secondCity = Substitute.For<ICity>();
            _fish = Substitute.For<IFish>();
            _fish.GetName().Returns("firstFish");
            _secondFish = Substitute.For<IFish>();
            _secondFish.GetName().Returns("secondFish");
        }
        
        [Test]
        public void BeAbleToAddCities()
        {
            _priceCalculator.AddCity(_city);
            _priceCalculator.AddCity(_secondCity);
            
            Assert.AreEqual(2, _priceCalculator.GetCitiesQuantity());
        }

        [Test]
        public void BeAbleToRemoveCities()
        {
            _priceCalculator.AddCity(_secondCity);
            _priceCalculator.AddCity(_city);
            _priceCalculator.RemoveCity(_city);
            
            Assert.AreEqual(1, _priceCalculator.GetCitiesQuantity());
        }

        [Test]
        public void BeAbleToAddFish()
        {
            _priceCalculator.AddFish(_fish);
            _priceCalculator.AddFish(_secondFish);
            
            Assert.AreEqual(2, _priceCalculator.GetCurrentFishQuantity());
        }
        
        [Test]
        public void BeAbleToRemoveFish()
        {
          _priceCalculator.AddFish(_secondFish);
          _priceCalculator.AddFish(_fish);

          _priceCalculator.RemoveFish(_fish);
          
          Assert.AreEqual(1, _priceCalculator.GetCurrentFishQuantity());
        }

        [Test]
        public void RemoveProductFromLocationsWhenRemovingProductFromCalculator()
        {
            _priceCalculator.AddCity(_city);
            _priceCalculator.AddCity(_secondCity);
            _priceCalculator.AddFish(_secondFish);
            _priceCalculator.AddFish(_fish);
            _priceCalculator.RemoveFish(_fish);

            _city.Received(1).RemoveFish(_fish);
            _secondCity.Received(1).RemoveFish(_fish);
        }
        
        [Test]
        public void BeAbleToAddProductsToTheVehicle()
        {
            _priceCalculator.AddFish(_fish);
            _priceCalculator.AddFish(_secondFish);
            
            _vehicle.ReceivedWithAnyArgs(2).AddFish(default);
        }
        
        [Test]
        public void ModifyFishQuantityInVehicle()
        {
            _vehicle.ChangeFishWeight(_fish, 50).Returns(true);
            
            bool result = _priceCalculator.ChangeFishQuantityInVehicle(_fish, 50);
            
            Assert.IsTrue(result);
        }

        [Test]
        public void AddANewProductToExistingLocations()
        {
            _priceCalculator.AddCity(_city);
            _priceCalculator.AddCity(_secondCity);
            
            _priceCalculator.AddFish(_fish);

            _city.Received(1).AddFish(_fish);
            _secondCity.Received(1).AddFish(_fish);
        }

        [Test]
        public void AddExistingProductsToNewLocation()
        {
            _priceCalculator.AddFish(_fish);
            _priceCalculator.AddCity(_city);

            _city.Received(1).AddFish(_fish);
        }

        [Test]
        public void CalculateBestSellingLocation()
        {
            _city.GetFishPrice(_fish).Returns(100);
            _city.GetFishPrice(_secondFish).Returns(200);
            _secondCity.GetFishPrice(_fish).Returns(80);
            _secondCity.GetFishPrice(_secondFish).Returns(160);
            _city.GetName().Returns("Madrid");

            Dictionary<IFish, int> vehicleProducts = new Dictionary<IFish, int>
            {
                { _fish, 50 },
                { _secondFish, 60 }
            };

            _vehicle.GetProductsAndQuantity().Returns(vehicleProducts);


            _priceCalculator.AddFish(_fish);
            _priceCalculator.AddFish(_secondFish);
            _priceCalculator.AddCity(_city);
            _priceCalculator.AddCity(_secondCity);
            _priceCalculator.ChangeFishQuantityInVehicle(_fish, 20);
            _priceCalculator.ChangeFishQuantityInVehicle(_secondFish, 30);
            
            Assert.AreEqual(_city, _priceCalculator.GetBestCityToSell());
        }
    }
}