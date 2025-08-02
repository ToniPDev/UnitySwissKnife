using System.Collections;

namespace PatronesDeComportamiento.Iterator
{
    public abstract class IteratorAggregate : IEnumerable
    {
        //Devuelve el iterator de otro iterator agregate
        public abstract IEnumerator GetEnumerator();
    }
}