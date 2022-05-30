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
        
        private ICalculatorPresenter _presenter;
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
        public void SetNewFishPanelNameInViewWhenAddingANewFish()
        {
            IFishPanelScript fishPanelScript = Substitute.For<IFishPanelScript>();
            _view.AddNewFishPanel("Bob").Returns(fishPanelScript);
            
            _presenter.AddNewFish("Bob");
            
            fishPanelScript.Received(1).SetName("Bob");
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
        public void AddNewFishToCalculator()
        {
            _presenter.AddNewFish("Bob");
            
            _priceCalculator.ReceivedWithAnyArgs(1).AddFish(default);
        }
        
        [Test]
        public void ModifyFishWeightInCalculator()
        {
        }
    }
}
