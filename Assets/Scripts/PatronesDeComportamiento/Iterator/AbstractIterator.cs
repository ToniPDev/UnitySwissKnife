using System.Collections;

namespace PatronesDeComportamiento.Iterator
{
    public abstract class AbstractIterator : IEnumerator
    {
        object IEnumerator.Current => Current();

        //Devuelve la llave del elemento actual
        public abstract int Key();
    
        //Devuelve el elemento actual
        public abstract object Current();
    
        public abstract bool MoveNext();
        public abstract void Reset();
    }
}
