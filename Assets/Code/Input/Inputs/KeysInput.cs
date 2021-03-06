using Code.Interfaces.Input;

namespace Code.Input.Inputs
{
    internal sealed class KeysInput
    {
        public static IUserKeyDownProxy Escape { get; private set; }
        public static IUserKeyDownProxy Jump { get; private set; }
        public static IUserKeyProxy Run { get; private set; }
        
        public KeysInput(IUserKeyDownProxy escape, IUserKeyDownProxy jump, IUserKeyProxy run)
        {
            Escape = escape;
            Jump = jump;
            Run = run;
        }
    }
}