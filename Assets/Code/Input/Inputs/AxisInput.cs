using Code.Interfaces.Input;

namespace Code.Input.Inputs
{
    internal class AxisInput
    {
        public static IUserAxisProxy Horizontal { get; private set; }

        public AxisInput(IUserAxisProxy horizontal)
        {
            Horizontal = horizontal;
        }
    }
}