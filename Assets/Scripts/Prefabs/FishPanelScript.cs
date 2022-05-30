using Scenes.Calculator_Scene.MVP;
using TMPro;
using UnityEngine;

namespace Prefabs
{
    public class FishPanelScript : MonoBehaviour, IFishPanelScript
    {
        [SerializeField] private TMP_Text fishName;
        [SerializeField] private TMP_Text quantity;
        private IFishPanelHandler _fishPanelHandler;

        public void SetName(string newFishName)
        {
            fishName.text = newFishName;
        }

        public void SetQuantity(string newQuantity)
        {
            quantity.text = newQuantity;
        }

        public void SetScriptToRemoveFish(IFishPanelHandler panelHandler)
        {
            _fishPanelHandler = panelHandler;
        }

        public void RemoveFish()
        {
            _fishPanelHandler.RemoveFish(GetName());
        }

        public void ModifyFishQuantity()
        {
            _fishPanelHandler.OpenModifyFishQuantityPanel(GetName());
        }

        public void DestroyPanel()
        {
            Destroy(gameObject);
        }

        public void ChangeQuantity()
        {
            //_presenter.ChangeFishQuantity(GetName());
        }

        private string GetName()
        {
            return fishName.text;
        }
    }
}
