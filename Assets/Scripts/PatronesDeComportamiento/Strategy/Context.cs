namespace PatronesDeComportamiento.Strategy
{
    public class Context
    {
        private IStrategy _strategy;

        public Context() { }
        
        public Context(IStrategy strategy) => _strategy = strategy;

        public void SetStrategy(IStrategy strategy) => _strategy = strategy;

        public void ExecuteStrategy(object data) => _strategy.ThrowAnimal(data);
    }
}