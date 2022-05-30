using Prefabs;
using TMPro;
using UnityEngine;


namespace Scenes.Calculator_Scene.MVP
{
    public class CalculatorView : MonoBehaviour, ICalculatorView
    {
        private ICalculatorPresenter _presenter;

        [SerializeField] private GameObject inputPanel;
        [SerializeField] private TMP_Text inputText;
        [SerializeField] private TMP_InputField inputField;

        [SerializeField] private GameObject panelToParentFishPrefab;
        [SerializeField] private GameObject fishPrefab;

        [SerializeField] private GameObject panelToParentCityPrefab;
        [SerializeField] private GameObject cityPrefab;
        
        [SerializeField] private GameObject firstPlacePanel;
        [SerializeField] private GameObject secondPlacePanel;
        [SerializeField] private GameObject thirdPlacePanel;
        [SerializeField] private GameObject top3CityPrefab;

        private CurrentInput _currentInput;

        private void Awake()
        {
            Vehicle vehicle = new Vehicle();
            PriceCalculator calculator = new PriceCalculator(vehicle);
            _presenter = new CalculatorPresenter(this, calculator);
        }

        public void AddNewFishButton()
        {
            _presenter.OpenAddNewFishInput();
            _currentInput = CurrentInput.Fish;
        }

        public void AddNewSellingSpotButton()
        {
            _presenter.OpenAddNewCityInput();
            _currentInput = CurrentInput.City;
        }

        public void CalculateBestSellingSpot()
        {
            _presenter.CalculateBestSellingSpot();
        }

        public IFishPanelScript AddNewFishPanel(string fishName)
        {
            GameObject newFishPanel = Instantiate(fishPrefab, panelToParentFishPrefab.transform);
            newFishPanel.name = fishName;
            FishPanelScript newFishPanelScript = newFishPanel.GetComponent<FishPanelScript>();
            return newFishPanelScript;
        }

        public ICityPanelScript AddNewCityPanel(string cityName)
        {
            GameObject newCityPanel = Instantiate(cityPrefab, panelToParentCityPrefab.transform);
            newCityPanel.name = cityName;
            CityPanelScript newCityPanelScript = newCityPanel.GetComponent<CityPanelScript>();
            return newCityPanelScript;
        }

        public void EnableFishInputPanel()
        {
            SetInputPanelToAskForFish();
            ActivateInputPanel();
        }
        
        public void EnableCityInputPanel()
        {
            SetInputPanelToAskForCity();
            ActivateInputPanel();
        }

        public void CloseInputPanel()
        {
            CleanInput();
            DeactivateInputPanel();
        }

        public void ConfirmInputButton()
        {
            if (_currentInput == CurrentInput.Fish)
            {
                _presenter.AddNewFish(inputField.text);
            }
            else
            {
                _presenter.AddNewCity(inputField.text);
            }
        }

        #region InputMethods

        private void SetInputPanelToAskForFish()
        {
            inputText.text = "Fish name:";
        }

        private void SetInputPanelToAskForCity()
        {
            inputText.text = "City name:";
        }

        private void CleanInput()
        {
            inputField.text = "";
        }

        private void ActivateInputPanel()
        {
            CloseInputPanel();
            inputPanel.SetActive(true);
        }

        private void DeactivateInputPanel()
        {
            inputPanel.SetActive(false);
        }

        #endregion
    }
}

public enum CurrentInput
{
    Fish,
    City
}