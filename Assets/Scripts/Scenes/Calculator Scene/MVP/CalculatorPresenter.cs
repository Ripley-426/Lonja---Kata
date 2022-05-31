using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using Prefabs;
using UnityEngine;

namespace Scenes.Calculator_Scene.MVP
{
    public class CalculatorPresenter : ICalculatorPresenter, IFishPanelHandler, ICityPanelHandler
    {
        private readonly ICalculatorView _view;
        private readonly IPriceCalculator _priceCalculator;
        private readonly Dictionary<IFish, IFishPanelScript> _fishInStock = new Dictionary<IFish, IFishPanelScript>();
        private readonly Dictionary<ICity, ICityPanelScript> _cities = new Dictionary<ICity, ICityPanelScript>();
        private ITop3CityPanelScript[] _currentTop3CityPanels;

        private string _currentFish = "";
        private string _currentCity = "";
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

        public void OpenModifyMaxWeightInput()
        {
            _view.EnableMaxWeightInputPanel();
        }

        public void OpenModifyFishQuantityInput(string fishName, string cityName)
        {
            _currentFish = fishName;
            _currentCity = cityName;
            if (cityName == "")
            {
                _view.EnableFishWeightInputPanel();
            }
            else
            {
                _view.EnableFishPriceInputPanel();
            }
        }

        public void OpenModifyCityDistanceInput(string cityName)
        {
            _currentCity = cityName;
            _view.EnableModifyCityDistanceInputPanel();
        }

        public void CalculateBestSellingSpot()
        {
            Top3Cities top3Cities = _priceCalculator.GetBestCityToSell();
            if (_currentTop3CityPanels.IsNullOrEmpty())
            {
                InstantiateAndFillTop3Cities(top3Cities);
            }
            else
            {
                FillTop3Cities(top3Cities);
            }
        }

        private void InstantiateAndFillTop3Cities(Top3Cities top3Cities)
        {
            _currentTop3CityPanels = new ITop3CityPanelScript[3];
            _currentTop3CityPanels[0] = _view.InstantiateFirstPlacePanel();
            _currentTop3CityPanels[1] = _view.InstantiateSecondPlacePanel();
            _currentTop3CityPanels[2] = _view.InstantiateThirdPlacePanel();

            FillTop3Cities(top3Cities);
        }

        private void FillTop3Cities(Top3Cities top3Cities)
        {
            _currentTop3CityPanels[0].Setup(1, top3Cities.firstPlaceName, top3Cities.firstPlaceEarnings.ToString());
            _currentTop3CityPanels[1].Setup(2, top3Cities.secondPlaceName, top3Cities.secondPlaceEarnings.ToString());
            _currentTop3CityPanels[2].Setup(3, top3Cities.thirdPlaceName, top3Cities.thirdPlaceEarnings.ToString());
        }

        public void AddNewFish(string fishName)
        {
            if (fishName == "") return;
            if (_fishInStock.Any(fish => fish.Key.GetName() == fishName)) return;
            
            IFish newFish = new Fish(fishName);
            
            IFishPanelScript newFishPanelScript = _view.AddNewFishPanel(fishName);
            newFishPanelScript.SetName(fishName);
            newFishPanelScript.SetScriptToHandlePanel(this);
            foreach (ICityPanelScript city in _cities.Values)
            {
                city.AddFish(fishName);
            }
            _fishInStock.Add(newFish, newFishPanelScript);

            _priceCalculator.AddFish(newFish);

            _view.CloseInputPanel();
        }

        public void ModifyFishWeight(string fishWeight)
        {
            _priceCalculator.ChangeFishQuantityInVehicle(GetFish(_currentFish), int.Parse(fishWeight));
            _fishInStock[GetFish(_currentFish)].SetQuantity(fishWeight);
            
            _view.CloseInputPanel();
        }

        public void ModifyFishPrice(string fishPrice)
        {
            _priceCalculator.ChangeFishPrice(GetCity(_currentCity), GetFish(_currentFish), int.Parse(fishPrice));
            _cities[GetCity(_currentCity)].SetFishPrice(_currentFish, fishPrice);
            _view.CloseInputPanel();
        }

        public void RemoveFish(string fishName)
        {
            RemoveFishPanel(fishName);
            RemoveFishPanelFromCities(fishName);
            RemoveFishFromCalculator(fishName);
            RemoveFishFromList(fishName);
        }

        private void RemoveFishPanel(string fishName)
        {
            _fishInStock[GetFish(fishName)].DestroyPanel();
        }

        private void RemoveFishPanelFromCities(string fishName)
        {
            foreach (ICityPanelScript cityPanelScript in _cities.Values)
            {
                cityPanelScript.RemoveFish(fishName);
            }
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

        public void ModifyMaxWeight(string newMaxWeight)
        {
            _priceCalculator.ChangeVehicleCapacity(int.Parse(newMaxWeight));
            _view.SetMaxWeight(newMaxWeight);
            _view.CloseInputPanel();
        }

        public void ModifyCityDistance(string distance)
        {
            if (distance == "") return;
            _priceCalculator.ChangeCityDistance(GetCity(_currentCity), int.Parse(distance));
            _cities[GetCity(_currentCity)].SetDistance(distance);
            _view.CloseInputPanel();
        }

        public void AddNewCity(string cityName)
        {
            if (cityName == "") return;
            if (_cities.Any(city => city.Key.GetName() == cityName)) return;
            
            ICity newCity = new City(cityName);
            
            ICityPanelScript newCityPanelScript = _view.AddNewCityPanel(cityName);
            newCityPanelScript.SetName(cityName);
            newCityPanelScript.SetScriptToModifyDistance(this, this);
            foreach (IFish fish in _fishInStock.Keys)
            {
                newCityPanelScript.AddFish(fish.GetName());
            }
            _cities.Add(newCity, newCityPanelScript);
            
            _priceCalculator.AddCity(newCity);
            
            _view.CloseInputPanel();
        }

        private ICity GetCity(string cityName)
        {
            return _cities.First(city => city.Key.GetName() == cityName).Key;
        }
    }
}