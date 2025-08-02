using System;
using UnityEngine.InputSystem;

namespace PlayerMovement.GameInputState
{
    public class PlayerInputState : InputState
    {
        public static EventHandler<InputAction.CallbackContext> MovePlayer,Attack,ChargeSpecial;
        
        public override void Move(InputAction.CallbackContext context) => MovePlayer.Invoke(this,context);

        public override void PressAttack(InputAction.CallbackContext context) => Attack.Invoke(this,context);

        public override void ChargeSpecialAttack(InputAction.CallbackContext context)
        {
            ChargeSpecial.Invoke(this, context);
            TransitionTo(InputContext.GetPointerState());
        }

        public override void LaunchSpecialAttack(InputAction.CallbackContext context) { }

        public override void CancelSpecialAttack(InputAction.CallbackContext context) { }

        public override void TransitionTo(InputState state) => InputContext.TransitionTo(state);
    }
}