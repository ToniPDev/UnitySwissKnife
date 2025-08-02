namespace PatronesDeComportamiento.Command
{
    /// <summary>
    /// Interfaz comando a aplicar en objeto para que se realize cierta accion.
    /// </summary>
    public interface ICommand
    {
        void Execute();
    }
}