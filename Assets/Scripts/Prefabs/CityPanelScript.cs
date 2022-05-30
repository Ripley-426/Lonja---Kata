using TMPro;
using UnityEngine;

namespace Prefabs
{
    public class CityPanelScript: MonoBehaviour, ICityPanelScript
    {
        [SerializeField] private TMP_Text cityName;

        public void SetName(string newCityName)
        {
            cityName.text = newCityName;
        }

        public void ChangeDistance()
        {
            
        }

        private string GetName()
        {
            return cityName.text;
        }
    }
}