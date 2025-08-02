namespace PatronesDeComportamiento.ChainOfResponsabillity
{
    //Declara el método para construir una cadena y otro para ejectura una petición
    public interface IDamageHandler
    {
        IDamageHandler SetNext(IDamageHandler nextDmgHandler);

        object Handle(object request);
    }
    
    //Clase opcional que se implementa en el controlador base.
    public abstract class AbstractDamageHandler : IDamageHandler
    {
        protected IDamageHandler _nextHandler;

        public IDamageHandler SetNext(IDamageHandler nextDmgHandler)
        {
           _nextHandler = nextDmgHandler;
           return _nextHandler;
        }

        public virtual object Handle(object request) => _nextHandler?.Handle(request);
    }
}