using System.Threading;
using UnityEngine;

namespace PatronesDeComportamiento.Memento
{
    //Funcion para guardar y cargar estados. Mantierne los estados que pueden cambiar.
    public class Originator
    {
        private string _state;

        public Originator(string state)
        {
            _state = state;
            Debug.Log("Originator: Mi estado es => " + _state);
        }
        
        //Tiene lógica que puede afectar al estado interno. El objeto que solicite cargar debe hacer un backup.
        public void DoSomething()
        {
            Debug.Log("Originator: Hago algo útil");
            _state = GenerateRandomString(30);
            Debug.Log("Originator: Mi estado ha cambiado a => " + _state);
        }
        
        private string GenerateRandomString(int length = 10)
        {
            const string allowedSymbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var result = string.Empty;

            while (length > 0)
            {
                result += allowedSymbols[Random.Range(0, allowedSymbols.Length)];

                Thread.Sleep(12);

                length--;
            }

            return result;
        }

        public IMemento Save() => new ConcreteMemento(_state);

        public void Restore(IMemento memento)
        {
            if(memento.GetType() != typeof(ConcreteMemento)) Debug.Log("Invalid memento class");
            _state = memento.GetState();
            Debug.Log("Originator: Mi estado ha cambiado a => " + _state);
        }
    }
}