using System;
using UnityEngine;

namespace Patrones_Creacionales.Abstract_Factory
{
    public class AbstractCreator : MonoBehaviour
    {
        public EntitiesType entitiesType;
        public enum EntitiesType
        {
            Armed,
            Disarmed
        }
        private void Start()
        {
            ClientMethod(GetEntitieType()); 
        }

        private IAbstractFactory GetEntitieType()
        {
            return entitiesType switch
            {
                EntitiesType.Armed => new ArmedEntitiesFactory(),
                EntitiesType.Disarmed => new DisarmedEntitiesFactory(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void ClientMethod(IAbstractFactory abstractFactory)
        {
            var npc = abstractFactory.CreateNpc(GameObject.CreatePrimitive(PrimitiveType.Sphere));
            var enemy = abstractFactory.CreateEnemy(GameObject.CreatePrimitive(PrimitiveType.Cube));
            
            npc.Initialize(enemy);
            enemy.Initialize();
        }
    }
}
