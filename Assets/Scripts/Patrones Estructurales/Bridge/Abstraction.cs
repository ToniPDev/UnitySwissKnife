using UnityEngine;

namespace Patrones_Estructurales.Bridge
{
    //Define la interfaz que delega el trabajo al objeto
    public class Abstraction
    {
        protected IImplementation _implementation;

        public Abstraction(IImplementation implementation)
        {
            _implementation = implementation;
        }

        public virtual string Operation()
        {
            return "Abstract: " + _implementation.OperationImplementation();
        }
    }
}
