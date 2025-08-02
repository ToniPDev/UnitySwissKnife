using System;
using System.Collections.Generic;
using UnityEngine;

namespace PatronesDeComportamiento.Visitor
{
    public class Visitor : MonoBehaviour
    {
        private void Start()
        {
            var components = new List<IComponent>(){new ConcreteComponentA(), new ConcreteComponentB()};

            var visitor1 = new ConcreteVisitor1();
            var visitor2 = new ConcreteVisitor2();

            foreach (var component in components)
            {
                Debug.Log("Accept visitor 1");
                component.Accept(visitor1);
                Debug.Log("Accept visitor 2");
                component.Accept(visitor2);
            }
        }
    }
}