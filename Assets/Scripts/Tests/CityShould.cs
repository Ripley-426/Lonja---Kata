using NSubstitute;
using NUnit.Framework;

namespace Tests
{
    public class CityShould
    {
        private ICity _city;
        private ICity _zeroDistanceCity;
        private IFish _fish;

        [SetUp]
        public void Setup()
        {
            _city = new City("Madrid");
            _city.SetDistance(800);
            _zeroDistanceCity = new City("Home");
            _fish = Substitute.For<IFish>();
        }
        
        [Test]
        public void HaveTheDistanceFromTheShop()
        {
            Assert.AreEqual(800, _city.GetDistance());
        }

        [Test]
        public void BeNamed()
        {
            Assert.AreEqual("Madrid", _city.GetName());
        }

        [Test]
        public void BeAbleToAddProducts()
        {
            _city.AddFish(_fish);
            
            Assert.AreEqual(1, _city.GetProductsQuantity());
        }

        [Test]
        public void BeAbleToSetPricesForEachProduct()
        {
            _zeroDistanceCity.AddFish(_fish);
            _zeroDistanceCity.SetProductPrice(_fish, 500);
            
            Assert.AreEqual(500, _zeroDistanceCity.GetFishPrice(_fish));
        }

        [Test]
        public void HaveADefaultPriceForEachProduct()
        {
            _zeroDistanceCity.AddFish(_fish);
            
            Assert.AreEqual(0, _zeroDistanceCity.GetFishPrice(_fish));
        }

        [Test]
        public void DeprecatePriceDependingOnDistance()
        {
            _city.AddFish(_fish);
            _city.SetProductPrice(_fish, 1000);
            
            Assert.AreEqual(920, _city.GetFishPrice(_fish));
        }
    }
}