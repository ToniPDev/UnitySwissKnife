using PlayerMovement.GameInputState.Input_Adapter;
using PlayerMovement.StaticClasses;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerMovement.GameInputState
{
    public class InputController : MonoBehaviour
    {
        private IInput Input { get; set; }
        
        private InputContext _inputContext;
        private InputState _playerInputState,_pointerInputState;
        
        private InputAction _moveAction,_attackAction,_specialAction;

        public void SetInput(IInput input) => Input = input;
        public void Initialize()
        {
            AssignInputActions();
            
            SubscribeToInputs();

            CreateAndInitializeInputStates();
        }

        private void AssignInputActions()
        {
            var master = new Master();
            master.Enable();
            
            _moveAction = Input.GetInput().Find(input => input.name == ActionNames.Movement());
            _attackAction = Input.GetInput().Find(input => input.name == ActionNames.Attack());
            _specialAction = Input.GetInput().Find(input => input.name == ActionNames.Special());
        }

        private void CreateAndInitializeInputStates()
        {
            _playerInputState = new PlayerInputState();
            _pointerInputState = new PointerInputState();
            
            _inputContext = new InputContext(_playerInputState);

            _inputContext.SetPlayerState(_playerInputState);
            _inputContext.SetPointerState(_pointerInputState);
            
            _playerInputState.SetContext(_inputContext);
        }

        private void SubscribeToInputs()
        {
            _moveAction.performed += Move;
            _attackAction.performed += Attack;
            _specialAction.performed += SpecialAttack;
            _specialAction.started += AimSpecialAttack;
            _specialAction.canceled += CancelSpecialAttack;
        }

        private void UnsubscribeToInputs()
        {
            _moveAction.performed -= Move;
            _attackAction.performed -= Attack;
            _specialAction.performed -= SpecialAttack;
            _specialAction.started -= AimSpecialAttack;
            _specialAction.canceled -= CancelSpecialAttack;
        }
        
        private void AimSpecialAttack(InputAction.CallbackContext obj) => _inputContext.AimSpecial(obj);

        private void CancelSpecialAttack(InputAction.CallbackContext obj) => _inputContext.CancelSpecial(obj);

        private void SpecialAttack(InputAction.CallbackContext obj) => _inputContext.LaunchSpecial(obj);

        private void Attack(InputAction.CallbackContext obj) => _inputContext.Attack(obj);

        private void Move(InputAction.CallbackContext obj) => _inputContext.MoveAction(obj);
    }
}