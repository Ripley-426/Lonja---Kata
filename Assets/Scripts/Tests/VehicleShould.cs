using NSubstitute;
using NUnit.Framework;

namespace Tests
{
    public class VehicleShould
    {
        private Vehicle _vehicle;
        private IFish _baseFish;

        [SetUp]
        public void Setup()
        {
            _vehicle = new Vehicle();
            _baseFish = Substitute.For<IFish>();
        }
        
        [Test]
        public void StartWithTheCorrectCapacity()
        {
            Assert.AreEqual(200, _vehicle.GetCapacity());
        }

        [Test]
        public void IncreaseWeightWhenAddingProducts()
        {
            _vehicle.AddFish(_baseFish);
            _vehicle.ChangeFishWeight(_baseFish, 50);
            
            Assert.AreEqual(50, _vehicle.GetWeight());
        }

        [Test]
        public void NotAcceptProductIfWeightWouldBeHigherThanCapacity()
        {
            _vehicle.LoadProduct(_baseFish, 300);
            
            Assert.AreEqual(0, _vehicle.GetWeight());
        }

        [Test]
        public void BeAbleToAddProducts()
        {
            _vehicle.AddFish(_baseFish);
            Assert.AreEqual(1, _vehicle.GetProductsQuantity());
        }
    }
}
