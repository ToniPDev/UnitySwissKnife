using UnityEngine;

namespace PatronesDeComportamiento.Strategy
{
    public class SquirrelStrategy : IStrategy
    {
        public void ThrowAnimal(object data)
        {
            Debug.Log("A cute looking Squirrel was throw making everyone who stares at it falling immediately in love with it");
            Debug.Log("The number of animals throwed is: " + data);
        }
    }
    
    public class CatStrategy : IStrategy
    {
        public void ThrowAnimal(object data)
        {
            Debug.Log("A cat was throw scratching everything that was around him");
            Debug.Log("The number of animals throwed is: " + data);
        }
    }
}