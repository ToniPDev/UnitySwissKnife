

namespace Patrones_Estructurales.Flyweight
{
    //Almacena una parte común a todos los objetos
    public class Flyweight
    {
        private Car _sharedState;

        public Flyweight(Car sharedState)
        {
            _sharedState = sharedState;
        }

        public void Operation(Car uniqueState)
        {
            
        }
    }
}