
using UnityEngine.InputSystem.LowLevel;

namespace PatronesDeComportamiento.State
{
    //Define la interfaz a los clientes, mantiene una referencia de la subclase que representa el estado del contexto
    public class MovementInputContext
    {
        private MovementInputState _inputState;

        public MovementInputContext(MovementInputState state) => TransitionTo(state);

        public void TransitionTo(MovementInputState state)
        {
            _inputState = state;
            _inputState.SetContext(this);
        }

        public void StateAction() => _inputState.Move();
        public void StateTransition() => _inputState.Transition();
    }
}