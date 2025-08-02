using UnityEngine;

namespace PatronesDeComportamiento.State
{
    public class MoveEnemyState : MovementInputState
    {
        public override void Move() => Debug.Log("Moving the enemy");
        public override void Transition() => MovementInputContext.TransitionTo(new MoveOnInterfaceState());
    }
}