using UnityEngine;

namespace Scenes.Calculator_Scene.MVP
{
    public class CalculatorPresenter : ICalculatorPresenter
    {
        private readonly ICalculatorView _view;
        public CalculatorPresenter(ICalculatorView calculatorView)
        {
            _view = calculatorView;
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
            Debug.Log("FISH: " + fishName);
        }

        public void AddNewCity(string cityName)
        {
            Debug.Log("CITY: " + cityName);
        }
    }
}