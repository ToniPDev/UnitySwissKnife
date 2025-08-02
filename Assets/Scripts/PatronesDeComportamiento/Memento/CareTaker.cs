using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PatronesDeComportamiento.Memento
{
    public class CareTaker
    {
        private List<IMemento> _mementos = new List<IMemento>();

        private Originator _originator;

        public CareTaker(Originator originator) => _originator = originator;

        public void Backup()
        {
            Debug.Log("Caretacker is saving originator state");
            _mementos.Add(_originator.Save());
        }

        public void Undo()
        {
            if(_mementos.Count == 0) return;

            var memento = _mementos.Last();
            _mementos.Remove(memento);
            
            Debug.Log("Caretaker is restoring state to => " + memento.GetName());

            try
            {
                _originator.Restore(memento);
            }
            catch (Exception)
            {
                Undo();
            }
        }

        public void ShowHistory()
        {
            Debug.Log("Here is the list of mementos");
            foreach (var memento in _mementos) Debug.Log(memento.GetName());
        }
    }
}