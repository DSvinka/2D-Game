namespace Code.Interfaces.Controllers
{
    internal interface IController
    {
        void Setup(SceneViews sceneViews);
        void ReSetup(SceneViews sceneViews);
    }
}