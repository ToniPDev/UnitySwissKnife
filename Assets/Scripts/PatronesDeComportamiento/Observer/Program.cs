using System;
using UnityEngine;

namespace PatronesDeComportamiento.Observer
{
    public class Program : MonoBehaviour
    {
        private void Start()
        {
            var subject = new Subject();
            var observerA = new ConcreteObserverA();
            var observerB = new ConcreteObserverB();
            
            subject.Attach(observerA);
            subject.Attach(observerB);
            
            subject.SomeBusinessLogic();
            subject.SomeBusinessLogic();
            
            subject.Detach(observerB);
            
            subject.SomeBusinessLogic();
        }
    }
}