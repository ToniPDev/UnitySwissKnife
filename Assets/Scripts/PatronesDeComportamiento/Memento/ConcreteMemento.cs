using System;

namespace PatronesDeComportamiento.Memento
{
    //Contiene la infraestructura para almacenar el estado del originador
    public class ConcreteMemento : IMemento
    {
        private readonly string _state;
        private readonly DateTime _dateTime;

        public ConcreteMemento(string state)
        {
            _state = state;
            _dateTime = DateTime.Now;
        }

        //Usado por el originador para restaurar
        public string GetState() => _state;
        
        //El resto es usado por el careTaker para displayear la info
        public string GetName() => _dateTime + " / " + _state;

        public DateTime GetDate() => _dateTime;
    }
}