using UnityEngine;

namespace Patrones_Creacionales.Abstract_Factory
{        
    // Esta factoria produce familias de enemigos desarmados
    public class DisarmedEntitiesFactory : IAbstractFactory
    {
        public IAbstractNpc CreateNpc(GameObject gameObject)
        {
            return gameObject.AddComponent<DisarmedNpc>();
        }

        public IAbstractEnemy CreateEnemy(GameObject gameObject)
        {
            return gameObject.AddComponent<DisarmedEnemy>();
        }
    }
}
