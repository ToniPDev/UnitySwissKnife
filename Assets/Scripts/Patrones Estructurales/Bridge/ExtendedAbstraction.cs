namespace Patrones_Estructurales.Bridge
{
    //Se puede extender la abstraccion sin cambiar las clases de implementacion
    public class ExtendedAbstraction : Abstraction
    {
        public ExtendedAbstraction(IImplementation implementation) : base(implementation)
        {
        }

        public override string Operation()
        {
            return "ExtendedAbstraction: " + _implementation.OperationImplementation();
        }
    }
}
