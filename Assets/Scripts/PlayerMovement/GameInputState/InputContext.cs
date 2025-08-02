using UnityEngine.InputSystem;

namespace PlayerMovement.GameInputState
{
    public class InputContext
    {
        private InputState _inputState;
        private InputState _playerInputState,_pointerInputState;

        public InputContext(InputState inputState) => _inputState = inputState;

        public void TransitionTo(InputState state)
        {
            _inputState = state;
            _inputState.SetContext(this);
        }

        public void MoveAction(InputAction.CallbackContext context) => _inputState.Move(context);
        public void AimSpecial(InputAction.CallbackContext context) => _inputState.ChargeSpecialAttack(context);
        public void CancelSpecial(InputAction.CallbackContext context) => _inputState.CancelSpecialAttack(context);
        public void LaunchSpecial(InputAction.CallbackContext context) => _inputState.LaunchSpecialAttack(context);
        public void Attack(InputAction.CallbackContext context) => _inputState.PressAttack(context);
        public InputState GetPointerState() => _pointerInputState;
        public InputState GetPlayerState() => _playerInputState;
        public void SetPlayerState(InputState state) => _playerInputState = state;
        public void SetPointerState(InputState state) => _pointerInputState = state;
    }
}