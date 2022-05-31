using System.Collections.Generic;
using Scenes.Calculator_Scene.MVP;
using TMPro;
using UnityEngine;

namespace Prefabs
{
    public class CityPanelScript: MonoBehaviour, ICityPanelScript
    {
        [SerializeField] private TMP_Text cityName;
        [SerializeField] private TMP_Text cityDistance;
        
        [SerializeField] private GameObject panelToParentFishPrefab;
        [SerializeField] private GameObject fishPrefab;

        private ICityPanelHandler _cityPanelHandler;
        private IFishPanelHandler _fishPanelHandler;
        private Dictionary<string, IFishPanelScript> _currentFish = new Dictionary<string, IFishPanelScript>();

        public void SetName(string newCityName)
        {
            cityName.text = newCityName;
        }

        public void AddFish(string fishName)
        {
            AddNewFishPanel(fishName);
        }

        public void RemoveFish(string fishName)
        {
            _currentFish[fishName].DestroyPanel();
            _currentFish.Remove(fishName);
        }

        private void AddNewFishPanel(string fishName)
        {
            GameObject newFishPanel = Instantiate(fishPrefab, panelToParentFishPrefab.transform);
            newFishPanel.name = fishName;
            FishPanelScript newFishPanelScript = newFishPanel.GetComponent<FishPanelScript>();
            newFishPanelScript.SetName(fishName);
            newFishPanelScript.DisableDeleteButtonIfInCity();
            newFishPanelScript.SetCity(cityName.text);
            newFishPanelScript.SetScriptToHandlePanel(_fishPanelHandler);
            _currentFish.Add(fishName, newFishPanelScript);
        }

        public void ModifyCityDistanceButton()
        {
            _cityPanelHandler.OpenModifyCityDistanceInput(GetName());
        }
        
        public void SetScriptToModifyDistance(ICityPanelHandler cityPanelHandler, IFishPanelHandler fishPanelHandler)
        {
            _cityPanelHandler = cityPanelHandler;
            _fishPanelHandler = fishPanelHandler;
        }

        public void SetDistance(string distance)
        {
            cityDistance.text = $"Distance: {distance}km";
        }

        public void SetFishPrice(string fishName, string fishPrice)
        {
            _currentFish[fishName].SetPrice(fishPrice);
        }

        private string GetName()
        {
            return cityName.text;
        }
    }
}