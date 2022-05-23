using UnityEngine;

namespace Scenes.Calculator_Scene.MVP
{
    public class CalculatorView : MonoBehaviour, ICalculatorView
    {
        private ICalculatorPresenter _presenter;

        private void Awake()
        {
            _presenter = new CalculatorPresenter(this);
        }

        public void AddNewFish()
        {
            _presenter.AddNewFish();
        }

        public void AddNewSellingSpot()
        {
            _presenter.AddNewSellingSpot();
        }

        public void CalculateBestSellingSpot()
        {
            _presenter.CalculateBestSellingSpot();
        }
    }
}