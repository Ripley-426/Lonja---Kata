using Scenes.Calculator_Scene.MVP;

namespace Prefabs
{
    public interface ICityPanelScript
    {
        void SetName(string newName);
        void AddFish(string fishName);
        void RemoveFish(string fishName);
        void SetScriptToModifyDistance(ICityPanelHandler cityPanelHandler, IFishPanelHandler fishPanelHandler);
        void SetDistance(string distance);
        void SetFishPrice(string currentFish, string fishPrice);
    }
}