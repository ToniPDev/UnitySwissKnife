namespace PatronesDeComportamiento.State
{
    public abstract class MovementInputState
    {
        protected MovementInputContext MovementInputContext;
        public void SetContext(MovementInputContext movementInputContext) => MovementInputContext = movementInputContext;
        public abstract void Move();
        public abstract void Transition();
    }
}