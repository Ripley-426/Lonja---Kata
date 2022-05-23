namespace Scenes.Calculator_Scene.MVP
{
    public class CalculatorPresenter : ICalculatorPresenter
    {
        private ICalculatorView _view;
        public CalculatorPresenter(ICalculatorView calculatorView)
        {
            _view = calculatorView;
        }

        public void AddNewFish()
        {
            throw new System.NotImplementedException();
        }

        public void AddNewSellingSpot()
        {
            throw new System.NotImplementedException();
        }

        public void CalculateBestSellingSpot()
        {
            throw new System.NotImplementedException();
        }
    }
}