using Code.Interfaces.Controllers;

namespace Code
{
    internal abstract class Controller: IController, IExecute, ILateExecute, IInitialization, ICleanup
    {
        public virtual void Setup(SceneViews sceneViews) {}
        public virtual void ReSetup(SceneViews sceneViews) {}

        public virtual void Initialization() {}
        public virtual void Execute(float deltaTime) {}
        public virtual void LateExecute(float deltaTime) {}
        public virtual void Cleanup() {}
    }
}