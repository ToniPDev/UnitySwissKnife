using UnityEngine;

namespace PatronesDeComportamiento.Observer
{
    public class ConcreteObserverA : IObserver
    {
        public void Update(ISubject subject)
        {
            if(subject.State < 3){Debug.Log("Concrete observer A has reacted");}
        }
    }
    
    public class ConcreteObserverB : IObserver
    {
        public void Update(ISubject subject)
        {
            if(subject.State is 0 or >= 2){Debug.Log("Concrete observer B has reacted");}
        }
    }
}