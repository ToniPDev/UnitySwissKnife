using System.Collections;
using System.Collections.Generic;

namespace PatronesDeComportamiento.Iterator
{
    public class WordsCollection : IteratorAggregate
    {
        private readonly List<string> _collection = new();

        private bool _direction;

        public void ReverseDirection() => _direction = !_direction;

        public List<string> GetItems() => _collection;
        
        public void AddItem(string item) => _collection.Add(item);
        
        public override IEnumerator GetEnumerator() => new AlphabeticalOrderIterator(this, _direction);
    }
}