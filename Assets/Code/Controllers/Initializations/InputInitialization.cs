using Code.Input;
using Code.Input.Inputs;

namespace Code.Controllers.Initializations
{
    internal sealed class InputInitialization
    {
        public InputInitialization()
        {
            var axisHorizontal = new AxisHorizontal();
            var axisInput = new AxisInput(axisHorizontal);
            
            var inputEscape = new InputEscape();
            var inputJump = new InputJump();
            var inputRun = new InputRun();
            var keysInput = new KeysInput(inputEscape, inputJump, inputRun);
        }
    }
}