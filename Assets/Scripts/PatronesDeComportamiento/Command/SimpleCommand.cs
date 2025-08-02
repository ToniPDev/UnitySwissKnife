using UnityEngine;

namespace PatronesDeComportamiento.Command
{
    /// <summary>
    /// Ejemplo de uso de comando simple el cual es capaz de realizar acciones. 
    /// </summary>
    public class SimpleCommand : ICommand
    {
        private string _playLoad;

        public SimpleCommand(string playLoad) => _playLoad = playLoad;

        public void Execute()
        {
            Debug.Log("Executed function on simple command");
        }
    }
}