using System;
using System.Collections.Generic;
using System.Threading;

namespace PatronesDeComportamiento.Observer
{
    public class Subject : ISubject
    {
        private List<IObserver> _observers = new List<IObserver>();
        private int _state;

        int ISubject.State
        {
            get => _state;
            set => _state = value;
        }

        public void Attach(IObserver observer) => _observers.Add(observer);

        public void Detach(IObserver observer) => _observers.Remove(observer);

        public void Notify()
        {
            foreach (var observer in _observers) observer.Update(this);
        }
        
        public void SomeBusinessLogic()
        {
            Console.WriteLine("\nSubject: I'm doing something important.");
            _state = new Random().Next(0, 10);

            Thread.Sleep(15);

            Console.WriteLine("Subject: My state has just changed to: " + _state);
            Notify();
        }
    }
}