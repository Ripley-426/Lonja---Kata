using TMPro;
using UnityEngine;

namespace Prefabs
{
    public class Top3CityPanelScript : MonoBehaviour, ITop3CityPanelScript
    {
        [SerializeField] private TMP_Text _place;
        [SerializeField] private TMP_Text _cityName;
        [SerializeField] private TMP_Text _earnings;

        public void Setup(int place, string city, string earnings)
        {
            _place.text = place switch
            {
                3 => "3rd place",
                2 => "2nd place",
                _ => "1st place"
            };
            _cityName.text = city;
            _earnings.text = $"Earnings: {earnings}";
        }

        public void DestroyPanel()
        {
            Destroy(gameObject);
        }
    }
}
