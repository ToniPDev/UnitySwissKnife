using UnityEngine;

namespace PatronesDeComportamiento.Visitor
{
    public class ConcreteVisitor1 : IVisitor
    {
        public void VisitConcreteComponentA(ConcreteComponentA element)
        {
            Debug.Log("Visitando el componente 1 :" + element.ExclusiveMethodOfComponentA());
        }

        public void VisitConcreteComponentB(ConcreteComponentB element)
        {
            Debug.Log("Visitando el componente 1 :" + element.ExclusiveMethodOfComponentB());
        }
    }
    
    public class ConcreteVisitor2 : IVisitor
    {
        public void VisitConcreteComponentA(ConcreteComponentA element)
        {
            Debug.Log("Visitando el componente 2 :" + element.ExclusiveMethodOfComponentA());
        }

        public void VisitConcreteComponentB(ConcreteComponentB element)
        {
            Debug.Log("Visitando el componente 2 :" + element.ExclusiveMethodOfComponentB());
        }
    }
}