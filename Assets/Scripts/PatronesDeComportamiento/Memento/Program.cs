using System;
using UnityEngine;

namespace PatronesDeComportamiento.Memento
{
    public class Program : MonoBehaviour
    {
        private void Start()
        {
            Originator originator = new Originator("Super-duper-super-puper-super.");
            CareTaker careTaker = new CareTaker(originator);
            
            careTaker.Backup();
            originator.DoSomething();
            
            careTaker.Backup();
            originator.DoSomething();
            
            careTaker.Backup();
            originator.DoSomething();
            
            careTaker.ShowHistory();
            
            Debug.Log("Dop undo : 1");
            careTaker.Undo();
            
            Debug.Log("Dop undo : 2");
            careTaker.Undo();
        }
    }
}