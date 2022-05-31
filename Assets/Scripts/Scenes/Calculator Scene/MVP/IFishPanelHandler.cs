namespace Scenes.Calculator_Scene.MVP
{
    public interface IFishPanelHandler
    {
        void RemoveFish(string fishName);
        void OpenModifyFishQuantityInput(string fishName, string cityName);
    }
}