using System;

namespace PatronesDeComportamiento.Memento
{
    //Contiene la forma de recivir la data del concrete memento. sin exponer el estadod el originador
    public interface IMemento
    {
        string GetName();
        string GetState();
        DateTime GetDate();
    }
}