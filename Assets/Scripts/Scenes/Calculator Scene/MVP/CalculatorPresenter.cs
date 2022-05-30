using System.Collections.Generic;
using Prefabs;
using UnityEngine;

namespace Scenes.Calculator_Scene.MVP
{
    public class CalculatorPresenter : ICalculatorPresenter
    {
        private readonly ICalculatorView _view;
        private readonly IPriceCalculator _priceCalculator;
        private readonly Dictionary<string, IFishPanelScript> _fishInStock = new Dictionary<string, IFishPanelScript>();
        private readonly Dictionary<string, ICityPanelScript> _cities = new Dictionary<string, ICityPanelScript>();
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

        public void CalculateBestSellingSpot()
        {
            throw new System.NotImplementedException();
        }

        public void AddNewFish(string fishName)
        {
            if (fishName == "") return;
            if (_fishInStock.ContainsKey(fishName)) return;
            
            IFishPanelScript newFishPanelScript = _view.AddNewFishPanel(fishName);
            newFishPanelScript.SetName(fishName);
            _fishInStock.Add(fishName, newFishPanelScript);

            IFish newFish = new Fish(fishName);
            _priceCalculator.AddFish(newFish);

            _view.CloseInputPanel();
            Debug.Log("FISH: " + fishName);
        }

        public void AddNewCity(string cityName)
        {
            if (cityName == "") return;
            if (_cities.ContainsKey(cityName)) return;
            
            ICityPanelScript newCityPanelScript = _view.AddNewCityPanel(cityName);
            newCityPanelScript.SetName(cityName);
            _cities.Add(cityName, newCityPanelScript);
            
            _view.CloseInputPanel();
            Debug.Log("CITY: " + cityName);
        }
    }
}