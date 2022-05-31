namespace Scenes.Calculator_Scene.MVP
{
    public interface ICalculatorPresenter
    {
        void OpenAddNewFishInput();
        void OpenAddNewCityInput();
        void CalculateBestSellingSpot();
        void AddNewFish(string fishName);
        void AddNewCity(string cityName);
        void ModifyFishWeight(string fishWeight);
        void OpenModifyMaxWeightInput();
        void ModifyMaxWeight(string inputFieldText);
        void ModifyCityDistance(string distance);
        void OpenModifyCityDistanceInput(string cityName);
        void ModifyFishPrice(string inputFieldText);
    }
}