using System;
using UnityEngine.InputSystem;

namespace PlayerMovement.GameInputState
{
    public class PointerInputState : InputState
    {
        public static EventHandler<InputAction.CallbackContext> MovePointer,LaunchSpecial,CancelSpecial; 
        public override void Move(InputAction.CallbackContext context) => MovePointer.Invoke(this,context);

        public override void PressAttack(InputAction.CallbackContext context) { }

        public override void ChargeSpecialAttack(InputAction.CallbackContext context) { }

        public override void LaunchSpecialAttack(InputAction.CallbackContext context)
        {
            LaunchSpecial.Invoke(this,context);
            TransitionTo(InputContext.GetPlayerState());
        }

        public override void CancelSpecialAttack(InputAction.CallbackContext context)
        {
            CancelSpecial.Invoke(this,context);
            TransitionTo(InputContext.GetPlayerState());
        }

        public override void TransitionTo(InputState state) => InputContext.TransitionTo(state);
    }
}