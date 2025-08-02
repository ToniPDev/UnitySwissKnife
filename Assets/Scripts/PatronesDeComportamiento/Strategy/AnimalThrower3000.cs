using System;
using UnityEngine;

namespace PatronesDeComportamiento.Strategy
{
    public class AnimalThrower3000 : MonoBehaviour
    {
        public Strategies strategies;
        public int numberOfAnimalsToThrow;
        public enum Strategies
        {
            Cat,
            Squirrel
        }
        private void Start()
        {
            var context = new Context(GetStrategy());
            
            context.ExecuteStrategy(numberOfAnimalsToThrow);
        }

        private IStrategy GetStrategy()
        {
            return strategies switch
            {
                Strategies.Cat => new CatStrategy(),
                Strategies.Squirrel => new SquirrelStrategy(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}