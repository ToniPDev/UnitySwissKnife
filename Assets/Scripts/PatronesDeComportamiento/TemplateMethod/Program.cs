using System;
using UnityEditor.PackageManager;
using UnityEngine;

namespace PatronesDeComportamiento.TemplateMethod
{
    public class Program : MonoBehaviour
    {
        private void Start()
        {
            var concreteClassA = new ConcreteClassA();
            var concreteClassB = new ConcreteClassB();
            
            concreteClassA.TemplateMethod();
            
            concreteClassB.TemplateMethod();
        }
    }
}