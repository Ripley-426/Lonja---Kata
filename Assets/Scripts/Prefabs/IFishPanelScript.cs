using Scenes.Calculator_Scene.MVP;

namespace Prefabs
{
    public interface IFishPanelScript
    {
        void SetName(string newName);
        void SetQuantity(string quantity);
        void DestroyPanel();
        void SetScriptToHandlePanel(IFishPanelHandler panelHandler);
        void SetPrice(string fishPrice);
    }
}