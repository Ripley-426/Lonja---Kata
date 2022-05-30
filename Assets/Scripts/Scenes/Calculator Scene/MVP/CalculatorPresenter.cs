using System.Collections.Generic;
using System.Linq;
using Prefabs;
using UnityEngine;

namespace Scenes.Calculator_Scene.MVP
{
    public class CalculatorPresenter : ICalculatorPresenter, IFishPanelHandler
    {
        private readonly ICalculatorView _view;
        private readonly IPriceCalculator _priceCalculator;
        private readonly Dictionary<IFish, IFishPanelScript> _fishInStock = new Dictionary<IFish, IFishPanelScript>();
        private readonly Dictionary<ICity, ICityPanelScript> _cities = new Dictionary<ICity, ICityPanelScript>();

        private string _currentFish = "";
        public CalculatorPresenter(ICalculatorView calculatorView, IPriceCalculator priceCalculator)
        {
            _view = calculatorView;
            _priceCalculator = priceCalculator;
        }

        public void OpenAddNewFishInput()
        {
            _view.EnableFishInputPanel();
        }

        public void OpenAddNewCityInput()
        {
            _view.EnableCityInputPanel();
        }

        public void OpenModifyFishQuantityPanel(string fishName)
        {
            _currentFish = fishName;
            _view.EnableFishWeightPanel();
        }

        public void CalculateBestSellingSpot()
        {
            throw new System.NotImplementedException();
        }

        public void AddNewFish(string fishName)
        {
            if (fishName == "") return;
            if (_fishInStock.Any(fish => fish.Key.GetName() == fishName)) return;
            
            IFish newFish = new Fish(fishName);
            
            IFishPanelScript newFishPanelScript = _view.AddNewFishPanel(fishName);
            newFishPanelScript.SetName(fishName);
            newFishPanelScript.SetScriptToRemoveFish(this);
            _fishInStock.Add(newFish, newFishPanelScript);

            _priceCalculator.AddFish(newFish);

            _view.CloseInputPanel();
            Debug.Log("FISH: " + fishName);
        }

        public void ModifyFishWeight(string fishWeight)
        {
            _priceCalculator.ChangeFishQuantityInVehicle(GetFish(_currentFish), int.Parse(fishWeight));
            _fishInStock[GetFish(_currentFish)].SetQuantity(fishWeight);
            
            _view.CloseInputPanel();
        }

        public void RemoveFish(string fishName)
        {
            RemoveFishPanel(fishName);
            RemoveFishFromCalculator(fishName);
            RemoveFishFromList(fishName);
        }

        private void RemoveFishPanel(string fishName)
        {
            _fishInStock[GetFish(fishName)].DestroyPanel();
        }

        private void RemoveFishFromCalculator(string fishName)
        {
            _priceCalculator.RemoveFish(GetFish(fishName));
        }

        private void RemoveFishFromList(string fishName)
        {
            _fishInStock.Remove(GetFish(fishName));
        }

        private IFish GetFish(string fishName)
        {
            return _fishInStock.First(fish => fish.Key.GetName() == fishName).Key;
        }

        public void AddNewCity(string cityName)
        {
            if (cityName == "") return;
            if (_cities.Any(city => city.Key.GetName() == cityName)) return;
            
            ICity newCity = new City(cityName);
            
            ICityPanelScript newCityPanelScript = _view.AddNewCityPanel(cityName);
            newCityPanelScript.SetName(cityName);
            _cities.Add(newCity, newCityPanelScript);
            
            _priceCalculator.AddCity(newCity);
            
            _view.CloseInputPanel();
            Debug.Log("CITY: " + cityName);
        }

        private ICity GetCity(string cityName)
        {
            return _cities.First(city => city.Key.GetName() == cityName).Key;
        }
    }
}