using UnityEngine.InputSystem;

namespace PlayerMovement.GameInputState
{
    public abstract class InputState
    {
        protected InputContext InputContext;
        public void SetContext(InputContext inputContext) => InputContext = inputContext;
        public abstract void Move(InputAction.CallbackContext context);
        public abstract void PressAttack(InputAction.CallbackContext context);
        public abstract void ChargeSpecialAttack(InputAction.CallbackContext context);
        public abstract void LaunchSpecialAttack(InputAction.CallbackContext context);
        public abstract void CancelSpecialAttack(InputAction.CallbackContext context);
        public abstract void TransitionTo(InputState state);
    }
}