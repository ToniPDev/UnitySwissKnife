using System;
using UnityEngine;

namespace Patrones_Creacionales.Builder
{
   public class EnemyInstantiator : MonoBehaviour
   {
      public EnemyType enemyType;
      public enum EnemyType
      {
         Orc,
         Elf
      }
      private void Start()
      {
         var director = new Director();
         var builder = GetBuilder();
         director.Builder = builder;
         
         director.BuildFullEnemy();
         foreach (var part in builder.GetEnemy().parts)
         {
            Debug.Log(part);
         }
      }

      private IEnemyBuilder GetBuilder()
      {
         return enemyType switch
         {
            EnemyType.Orc => new OrcBuilder(),
            EnemyType.Elf => new ElfBuilder(),
            _ => throw new ArgumentOutOfRangeException()
         };
      }
   }
}
