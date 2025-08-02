using UnityEngine;

namespace Patrones_Creacionales.Abstract_Factory
{
    // Esta factoria produce familias de npcs y enemigos garantizando que las creaciones sean compatibles(estan armados)
    //Se crean productos abstractos que se definen luego
    public class ArmedEntitiesFactory : IAbstractFactory
    {
        public IAbstractNpc CreateNpc(GameObject gameObject)
        {
            return gameObject.AddComponent<NpcArmed>();
        }

        public IAbstractEnemy CreateEnemy(GameObject gameObject)
        {
            return gameObject.AddComponent<EnemyArmed>();
        }
    }
}
