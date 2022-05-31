using System;
using Prefabs;
using TMPro;
using UnityEngine;


namespace Scenes.Calculator_Scene.MVP
{
    public class CalculatorView : MonoBehaviour, ICalculatorView
    {
        private ICalculatorPresenter _presenter;

        [SerializeField] private GameObject inputPanel;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private TMP_Text inputText;
        [SerializeField] private TMP_Text placeholderText;

        [SerializeField] private TMP_Text maxWeightText;

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
        }

        public void AddNewSellingSpotButton()
        {
            _presenter.OpenAddNewCityInput();
        }

        public void CalculateBestSellingSpotButton()
        {
            _presenter.CalculateBestSellingSpot();
        }

        public void ModifyMaxWeightButton()
        {
            _presenter.OpenModifyMaxWeightInput();
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
            _currentInput = CurrentInput.Fish;
            InitializeAndActivateInputPanel();
        }

        public void EnableCityInputPanel()
        {
            _currentInput = CurrentInput.City;
            InitializeAndActivateInputPanel();
        }

        public void EnableFishWeightInputPanel()
        {
            _currentInput = CurrentInput.ModifyFishWeight;
            InitializeAndActivateInputPanel();
        }

        public void EnableMaxWeightInputPanel()
        {
            _currentInput = CurrentInput.MaxWeight;
            InitializeAndActivateInputPanel();
        }

        public void SetMaxWeight(string weight)
        {
            maxWeightText.text = $"Max Weight: {weight}kg";
        }

        public void EnableModifyCityDistanceInputPanel()
        {
            _currentInput = CurrentInput.CityDistance;
            InitializeAndActivateInputPanel();
        }

        public void EnableFishPriceInputPanel()
        {
            _currentInput = CurrentInput.FishPrice;
            InitializeAndActivateInputPanel();
        }

        private void InitializeAndActivateInputPanel()
        {
            InitializeInputPanel();
            ActivateInputPanel();
        }

        public void CloseInputPanel()
        {
            CleanInput();
            DeactivateInputPanel();
        }

        public void ConfirmInputButton()
        {
            switch (_currentInput)
            {
                case CurrentInput.Fish:
                    _presenter.AddNewFish(inputField.text);
                    break;
                case CurrentInput.ModifyFishWeight:
                    _presenter.ModifyFishWeight(inputField.text);
                    break;
                case CurrentInput.City:
                    _presenter.AddNewCity(inputField.text);
                    break;
                case CurrentInput.MaxWeight:
                    _presenter.ModifyMaxWeight(inputField.text);
                    break;
                case CurrentInput.CityDistance:
                    _presenter.ModifyCityDistance(inputField.text);
                    break;
                case CurrentInput.FishPrice:
                    _presenter.ModifyFishPrice(inputField.text);
                    break;
                default:
                    break;
            }
        }

        #region InputMethods

        private void InitializeInputPanel()
        {
            CloseInputPanel();
            switch (_currentInput)
            {
                case CurrentInput.Fish:
                    SetInputPanelToAskForFish();
                    break;
                case CurrentInput.City:
                    SetInputPanelToAskForCity();
                    break;
                case CurrentInput.ModifyFishWeight:
                    SetInputPanelToAskForFishWeight();
                    break;
                case CurrentInput.MaxWeight:
                    SetInputPanelToAskForMaxWeight();
                    break;
                case CurrentInput.CityDistance:
                    SetInputPanelToAskForCityDistance();
                    break;
                case CurrentInput.FishPrice:
                    SetInputPanelToAskForFishPrice();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetInputPanelToAskForFish()
        {
            inputText.text = "Fish name:";
        }

        private void SetInputPanelToAskForCity()
        {
            inputText.text = "City name:";
        }
        
        private void SetInputPanelToAskForFishWeight()
        {
            inputText.text = "Fish weight:";
            placeholderText.text = "Enter fish weight...";
            inputField.contentType = TMP_InputField.ContentType.IntegerNumber;
        }

        private void SetInputPanelToAskForMaxWeight()
        {
            inputText.text = "Max weight:";
            placeholderText.text = "Enter max weight...";
            inputField.contentType = TMP_InputField.ContentType.IntegerNumber;
        }

        private void SetInputPanelToAskForFishPrice()
        {
            inputText.text = "Fish price:";
            placeholderText.text = "Enter fish price...";
            inputField.contentType = TMP_InputField.ContentType.IntegerNumber;
        }

        private void SetInputPanelToAskForCityDistance()
        {
            inputText.text = "City Distance:";
            placeholderText.text = "Enter city distance...";
            inputField.contentType = TMP_InputField.ContentType.IntegerNumber;
        }

        private void CleanInput()
        {
            inputField.text = "";
            placeholderText.text = "Enter name...";
            inputField.contentType = TMP_InputField.ContentType.Name;
        }

        private void ActivateInputPanel()
        {
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
    ModifyFishWeight,
    MaxWeight,
    City,
    CityDistance,
    FishPrice
}