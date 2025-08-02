namespace PatronesDeComportamiento.Iterator
{
    public class AlphabeticalOrderIterator : AbstractIterator
    {
        private readonly WordsCollection _collection;

        private int _position = -1;

        private readonly bool _reverse;

        public AlphabeticalOrderIterator(WordsCollection collection, bool reverse)
        {
            _collection = collection;
            _reverse = reverse;

            if (reverse) _position = collection.GetItems().Count;
        }
        public override int Key() => _position;
        public override object Current() => _collection.GetItems()[_position];

        public override bool MoveNext()
        {
            var updatedPosition = _position + (_reverse ? -1 : 1);
            
            if (updatedPosition >= 0 && updatedPosition < _collection.GetItems().Count)
            {
                _position = updatedPosition;
                return true;
            }

            return false;
        }

        public override void Reset() => _position = _reverse ? _collection.GetItems().Count - 1 : 0;
    }
}