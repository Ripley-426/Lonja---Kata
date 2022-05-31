using Prefabs;
using UnityEngine;

namespace Scenes.Calculator_Scene.MVP
{
    public interface ICalculatorView
    {
        void EnableFishInputPanel();
        void EnableCityInputPanel();
        IFishPanelScript AddNewFishPanel(string fishName);
        ICityPanelScript AddNewCityPanel(string cityName);
        void CloseInputPanel();
        void EnableFishWeightInputPanel();
        void EnableMaxWeightInputPanel();
        void SetMaxWeight(string weight);
        void EnableModifyCityDistanceInputPanel();
        void EnableFishPriceInputPanel();
    }
}