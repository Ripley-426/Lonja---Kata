using NSubstitute;
using NUnit.Framework;

namespace Tests
{
    public class VehicleShould
    {
        private Vehicle _vehicle;
        private IProduct _baseProduct;

        [SetUp]
        public void Setup()
        {
            _vehicle = new Vehicle();
            _baseProduct = Substitute.For<IProduct>();
        }
        
        [Test]
        public void StartWithTheCorrectCapacity()
        {
            Assert.AreEqual(200, _vehicle.GetCapacity());
        }

        [Test]
        public void IncreaseWeightWhenAddingProducts()
        {
            _vehicle.LoadProduct(_baseProduct, 50);
            
            Assert.AreEqual(50, _vehicle.GetWeight());
        }

        [Test]
        public void NotAcceptProductIfWeightWouldBeHigherThanCapacity()
        {
            _vehicle.LoadProduct(_baseProduct, 300);
            
            Assert.AreEqual(0, _vehicle.GetWeight());
        }

        [Test]
        public void BeAbleToAddProducts()
        {
            _vehicle.AddProduct(_baseProduct, 30);
            Assert.AreEqual(1, _vehicle.GetProductsQuantity());
        }
    }
}
