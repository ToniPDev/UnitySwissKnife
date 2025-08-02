using UnityEngine;

namespace PatronesDeComportamiento.Command
{
    /// <summary>
    /// Clase encargada de realizar serie de acciones concretas.
    /// </summary>
    public class Receiver
    {
        public void DoSomething(string a) => Debug.Log("Working on string a");
        
        public void DoSomethingElse(string b) => Debug.Log("Working on string b");
    }
}