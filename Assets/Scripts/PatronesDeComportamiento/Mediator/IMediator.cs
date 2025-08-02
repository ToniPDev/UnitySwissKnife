namespace PatronesDeComportamiento.Mediator
{
    //La interfaz declara métodos usados por componeentes para notificar al mediador de varios eventos.
    public interface IMediator
    {
        void Notify(object sender, string ev);
    }
}