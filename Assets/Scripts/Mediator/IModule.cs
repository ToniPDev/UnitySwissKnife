namespace Mediator
{
    /// <summary>
    /// Interfaz módulo para la implementacion de los módulos principales del jugador.
    /// </summary>
    public interface IModule
    {
        public PlayerMediator PlayerMediator { get; set; }
        public virtual void Init(PlayerMediator mediator) { }
        //public virtual void Tick() { }
        public virtual void Dispose() { }
    }
}
