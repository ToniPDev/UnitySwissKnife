using System;
using UnityEngine;

namespace PatronesDeComportamiento.Iterator
{
    public class Program : MonoBehaviour
    {
        private void Start()
        {
            var collection = new WordsCollection();
            collection.AddItem("First");
            collection.AddItem("Second");
            collection.AddItem("Third");
            collection.AddItem("Fourth");
            
            Debug.Log("Recorrido normal");

            foreach (var element in collection) Debug.Log(element);

            Debug.Log("Recorrido inverso");

            collection.ReverseDirection();

            foreach (var element in collection) Debug.Log(element);
            
            var enumerator = (AbstractIterator)collection.GetEnumerator();

            enumerator.Reset();
            
            Debug.Log("Estamos en la posicion " + enumerator.Key() + " porque vamos al revés vaya");
            
            Debug.Log("El elemento actual despues de recorrerlo es " + enumerator.Current() + ", Lo adelanto una posicion " + enumerator.MoveNext() + " " + enumerator.Current());
            
            Debug.Log("I ahora en la posicion " + enumerator.Key());
            
            Debug.Log("Puedo moverme adelante¿? " + enumerator.MoveNext() + " " + enumerator.Current());
        }
    }
}