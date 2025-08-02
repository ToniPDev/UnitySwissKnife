namespace PatronesDeComportamiento.Command
{
    /// <summary>
    /// Clase que contiene comandos a realizar y funciones que ejecutan dichos comandos.
    /// </summary>
    public class CommandInvoker
    {
        private ICommand _onStart,_onEnd;

        public void SetOnStart(ICommand command) => _onStart = command;
        
        public void SetOnEnd(ICommand command) => _onEnd = command;

        public void DoSomethingImportant()
        {
            _onStart?.Execute();
            _onEnd?.Execute();
        }
    }
}