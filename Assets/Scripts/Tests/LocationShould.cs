using NSubstitute;
using NUnit.Framework;

namespace Tests
{
    public class LocationShould
    {
        private ILocation _location;
        private IProduct _product;

        [SetUp]
        public void Setup()
        {
            _location = new Location(800, "Madrid");
            _product = Substitute.For<IProduct>();
        }
        
        [Test]
        public void HaveTheDistanceFromTheShop()
        {
            
            Assert.AreEqual(800, _location.GetDistance());
        }

        [Test]
        public void BeNamed()
        {
            Assert.AreEqual("Madrid", _location.GetName());
        }

        [Test]
        public void BeAbleToAddProducts()
        {
            _location.AddProduct(_product);
            
            Assert.AreEqual(1, _location.GetProductsQuantity());
        }

        [Test]
        public void BeAbleToSetPricesForEachProduct()
        {
            _location.AddProduct(_product);
            _location.SetProductPrice(_product, 500);
            
            Assert.AreEqual(500, _location.GetProductPrice(_product));
        }

        [Test]
        public void HaveADefaultPriceForEachProduct()
        {
            _location.AddProduct(_product);
            
            Assert.AreEqual(0, _location.GetProductPrice(_product));
        }
    }
}