using System;
using UnityEngine;

namespace PatronesDeComportamiento.Mediator
{
    public class EnemyBuilder : MonoBehaviour
    {
        private void Start()
        {
            var orcGo = GameObject.CreatePrimitive(PrimitiveType.Cube);
            var elfGo = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            var orc = orcGo.AddComponent<Orc>();
            var elf = elfGo.AddComponent<Elf>();

            var enemyMediator = new EnemyMediator(orc, elf);

            orc.Attack();
            elf.Sleep();
        }
    }
}