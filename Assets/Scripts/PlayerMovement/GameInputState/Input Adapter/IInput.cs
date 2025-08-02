using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace PlayerMovement.GameInputState.Input_Adapter
{
    public interface IInput
    {
        public List<InputAction> GetInput();
    }

    public class XboxInputAdapter : IInput
    {
        public List<InputAction> GetInput()
        {
            var master = new Master();
            master.Enable();
            
            return new List<InputAction>
            { master.PlayerInputXbox.Dash, master.PlayerInputXbox.Movement, master.PlayerInputXbox.SwordAttack, master.PlayerInputXbox.SpecialAttack };
        }
    }

    public class KeyboardInputAdapter : IInput
    { 
        public List<InputAction> GetInput()
        {
            var master = new Master();
            master.Enable();
            
            return new List<InputAction>
            {
                master.PlayerInputKeyboard.Dash, master.PlayerInputKeyboard.Movement,
                master.PlayerInputKeyboard.SwordAttack,master.PlayerInputKeyboard.SpecialAttack
            };
        }
    }
}
