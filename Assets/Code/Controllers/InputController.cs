using Code.Input.Inputs;
using Code.Interfaces.Controllers;

namespace Code.Controllers
{
    internal sealed class InputController : Controller
    {
        public override void Execute(float deltaTime)
        {
            AxisInput.Horizontal.GetAxis();

            KeysInput.Escape.GetKeyDown();
            KeysInput.Jump.GetKeyDown();
            KeysInput.Run.GetKey();
        }
    }
}