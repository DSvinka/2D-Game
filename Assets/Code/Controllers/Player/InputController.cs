using Code.Input.Inputs;

namespace Code.Controllers.Player
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