using UnityEngine;

namespace PatronesDeComportamiento.Command
{
    public class Program : MonoBehaviour
    {
        private void Start()
        {
            CommandInvoker invoke = new CommandInvoker();
            invoke.SetOnStart(new SimpleCommand("Holap funciones simples realizadas"));
            
            Receiver receiver = new Receiver();
            invoke.SetOnEnd(new ComplexCommand(receiver,"Send email","Save report"));
            
            invoke.DoSomethingImportant();
        }
    }
}