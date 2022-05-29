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
            _presenter = new CalculatorPresenter(this);
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

        private void SetInputPanelToAskForFish()
        {
            inputText.text = "Fish name:";
        }

        private void SetInputPanelToAskForCity()
        {
            inputText.text = "City name:";
        }

        public void CloseInputPanel()
        {
            CleanInput();
            DeactivateInputPanel();
        }

        private void CleanInput()
        {
            inputField.text = "";
        }

        private void ActivateInputPanel()
        {
            inputPanel.SetActive(true);
        }

        private void DeactivateInputPanel()
        {
            inputPanel.SetActive(false);
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
    }
}

public enum CurrentInput
{
    Fish,
    City
}