using Code.Input.Inputs;
using Code.Interfaces;
using Code.Interfaces.Controllers;

namespace Code.Controllers
{
    internal sealed class InputController : IController, IUpdate
    {
        public void Update(float deltaTime)
        {
            AxisInput.Horizontal.GetAxis();

            KeysInput.Escape.GetKeyDown();
            KeysInput.Jump.GetKeyDown();
            KeysInput.Run.GetKey();

            
            
        }
    }
}