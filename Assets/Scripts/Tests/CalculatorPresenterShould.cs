using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using Prefabs;
using Scenes.Calculator_Scene.MVP;
using UnityEngine;

namespace Tests
{
    public class CalculatorPresenterShould
    {
        
        private CalculatorPresenter _presenter;
        private ICalculatorView _view;
        private IPriceCalculator _priceCalculator;
        private IFish _fish;

        [SetUp]
        public void Setup()
        {
            _priceCalculator = Substitute.For<IPriceCalculator>();
            _view = Substitute.For<ICalculatorView>();
            _fish = Substitute.For<IFish>();
            _presenter = new CalculatorPresenter(_view, _priceCalculator);
        }
        
        [Test]
        public void EnableNewFishPanelInViewWhenButtonIsPressed()
        {
            _presenter.OpenAddNewFishInput();
            
            _view.Received(1).EnableFishInputPanel();
        }
        
        [Test]
        public void EnableNewCityPanelInViewWhenButtonIsPressed()
        {
            _presenter.OpenAddNewCityInput();
            
            _view.Received(1).EnableCityInputPanel();
        }
        
        [Test]
        public void InstantiateNewFishPanelInViewWhenAddingANewFish()
        {
            _presenter.AddNewFish("Bob");
            
            _view.Received(1).AddNewFishPanel("Bob");
        }
        
        [Test]
        public void NotInstantiateNewFishPanelInViewWhenAddingANewFishWithoutName()
        {
            _presenter.AddNewFish("");
            
            _view.Received(0).AddNewFishPanel("Bob");
        }
        
        [Test]
        public void NotAddANewFishWithADuplicateName()
        {
            _presenter.AddNewFish("Bob");
            _presenter.AddNewFish("Bob");
            
            _view.Received(1).AddNewFishPanel("Bob");
        }
        
        [Test]
        public void SetNewFishPanelNameInViewWhenAddingANewFish()
        {
            IFishPanelScript fishPanelScript = Substitute.For<IFishPanelScript>();
            _view.AddNewFishPanel("Bob").Returns(fishPanelScript);
            
            _presenter.AddNewFish("Bob");
            
            fishPanelScript.Received(1).SetName("Bob");
        }
        
        [Test]
        public void AddNewFishToCalculator()
        {
            _presenter.AddNewFish("Bob");
            
            _priceCalculator.ReceivedWithAnyArgs(1).AddFish(default);
        }
        
        [Test]
        public void RemoveFishPanelWhenRemovingFish()
        {
            IFishPanelScript fishPanelScript = Substitute.For<IFishPanelScript>();
            _view.AddNewFishPanel("Bob").Returns(fishPanelScript);
            
            _presenter.AddNewFish("Bob");
            _presenter.RemoveFish("Bob");

            fishPanelScript.Received(1).DestroyPanel();
        }
        
        [Test]
        public void RemoveFishFromCalculatorWhenRemovingFish()
        {
            IFishPanelScript fishPanelScript = Substitute.For<IFishPanelScript>();
            _view.AddNewFishPanel("Bob").Returns(fishPanelScript);
            
            _presenter.AddNewFish("Bob");
            _presenter.RemoveFish("Bob");

            _priceCalculator.ReceivedWithAnyArgs(1).RemoveFish(default);
        }
        
        [Test]
        public void InstantiateModifyFishQuantityPanel()
        {
            _presenter.AddNewFish("Bob");
            _presenter.OpenModifyFishQuantityInput("Bob", "");
            
            _view.Received(1).EnableFishWeightInputPanel();
        }
        
        [Test]
        public void ModifyFishQuantityInCalculator()
        {
            _presenter.AddNewFish("Bob");
            _presenter.OpenModifyFishQuantityInput("Bob", "");
            _presenter.ModifyFishWeight("50");

            _priceCalculator.ReceivedWithAnyArgs(1).ChangeFishQuantityInVehicle(default, 50);
        }
        
        [Test]
        public void InstantiateModifyMaxWeightPanel()
        {
            _presenter.OpenModifyMaxWeightInput();
            
            _view.Received(1).EnableMaxWeightInputPanel();
        }
        
        [Test]
        public void ModifyMaxWeightInPanel()
        {
            _presenter.ModifyMaxWeight(300.ToString());
            
            _view.Received(1).SetMaxWeight(300.ToString());
        }
        
        [Test]
        public void ModifyMaxWeightInCalculator()
        {
            _presenter.ModifyMaxWeight(300.ToString());

            _priceCalculator.Received(1).ChangeVehicleCapacity(300);
        }
        
        [Test]
        public void InstantiateNewCityPanelInViewWhenAddingANewCity()
        {
            _presenter.AddNewCity("Bob");
            
            _view.Received(1).AddNewCityPanel("Bob");
        }
        
        [Test]
        public void NotInstantiateNewCityPanelInViewWhenAddingANewCityWithoutName()
        {
            _presenter.AddNewFish("");
            
            _view.Received(0).AddNewCityPanel("Bob");
        }
        
        [Test]
        public void NotAddANewCityWithADuplicateName()
        {
            _presenter.AddNewCity("Bob");
            _presenter.AddNewCity("Bob");
            
            _view.Received(1).AddNewCityPanel("Bob");
        }
        
        [Test]
        public void SetNewCityPanelNameInViewWhenAddingANewCity()
        {
            ICityPanelScript cityPanelScript = Substitute.For<ICityPanelScript>();
            _view.AddNewCityPanel("Bob").Returns(cityPanelScript);
            
            _presenter.AddNewCity("Bob");
            
            cityPanelScript.Received(1).SetName("Bob");
        }
        
        [Test]
        public void AddNewCityToCalculator()
        {
            _presenter.AddNewCity("Bob");
            
            _priceCalculator.ReceivedWithAnyArgs(1).AddCity(default);
        }

        [Test]
        public void IncludeAllExistingFishToNewCities()
        {
            IFishPanelScript fishPanelScript = Substitute.For<IFishPanelScript>();
            _view.AddNewFishPanel("Bob").Returns(fishPanelScript);
            
            ICityPanelScript cityPanelScript = Substitute.For<ICityPanelScript>();
            _view.AddNewCityPanel("Madrid").Returns(cityPanelScript);
            
            _presenter.AddNewFish("Bob");
            _presenter.AddNewCity("Madrid");

            cityPanelScript.Received(1).AddFish("Bob");
        }
        
        [Test]
        public void IncludeNewFishToExistingCities()
        {
            IFishPanelScript fishPanelScript = Substitute.For<IFishPanelScript>();
            _view.AddNewFishPanel("Bob").Returns(fishPanelScript);
            
            ICityPanelScript cityPanelScript = Substitute.For<ICityPanelScript>();
            _view.AddNewCityPanel("Madrid").Returns(cityPanelScript);
            
            _presenter.AddNewCity("Madrid");
            _presenter.AddNewFish("Bob");

            cityPanelScript.Received(1).AddFish("Bob");
        }

        [Test]
        public void RemoveDeletedFishFromExistingCities()
        {
            IFishPanelScript fishPanelScript = Substitute.For<IFishPanelScript>();
            _view.AddNewFishPanel("Bob").Returns(fishPanelScript);
            
            ICityPanelScript cityPanelScript = Substitute.For<ICityPanelScript>();
            _view.AddNewCityPanel("Madrid").Returns(cityPanelScript);
            
            _presenter.AddNewCity("Madrid");
            _presenter.AddNewFish("Bob");
            
            _presenter.RemoveFish("Bob");

            cityPanelScript.Received(1).RemoveFish("Bob");
        }
        
        [Test]
        public void InstantiateModifyCityDistancePanel()
        {
            ICityPanelScript cityPanelScript = Substitute.For<ICityPanelScript>();
            _view.AddNewCityPanel("Madrid").Returns(cityPanelScript);
            
            _presenter.OpenModifyCityDistanceInput("Madrid");
            
            _view.Received(1).EnableModifyCityDistanceInputPanel();
        }
    }
}
