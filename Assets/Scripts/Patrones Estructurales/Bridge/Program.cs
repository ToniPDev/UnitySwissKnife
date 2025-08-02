using System;
using UnityEngine;

namespace Patrones_Estructurales.Bridge
{
    public class Program : MonoBehaviour
    {
        public ImplementationType implementationType;
        public enum ImplementationType
        {
           ImplementationA,
           ImplementationB
        }

        private IImplementation GetImplementation()
        {
            return implementationType switch
            {
                ImplementationType.ImplementationA => new ConcreteImplementationA(),
                ImplementationType.ImplementationB => new ConcreteImplementationB(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void ClientCode(Abstraction abstraction) => Debug.Log(abstraction.Operation());

        private void Start()
        { 
            var abstraction = new Abstraction(GetImplementation());
            ClientCode(abstraction);
        }
    }
}
