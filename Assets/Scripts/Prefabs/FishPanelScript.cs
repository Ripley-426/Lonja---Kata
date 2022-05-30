using TMPro;
using UnityEngine;

namespace Prefabs
{
    public class FishPanelScript : MonoBehaviour, IFishPanelScript
    {
        [SerializeField] private TMP_Text fishName;

        public void SetName(string newFishName)
        {
            fishName.text = newFishName;
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
