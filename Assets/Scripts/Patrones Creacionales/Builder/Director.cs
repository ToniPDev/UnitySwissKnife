namespace Patrones_Creacionales.Builder
{
   public class Director
   {
      private IEnemyBuilder _enemyBuilder;

      public IEnemyBuilder Builder
      {
         set => _enemyBuilder = value;
      }

      public void BuildFullEnemy()
      {
         _enemyBuilder.BuildHead();
         _enemyBuilder.BuildBody();
         _enemyBuilder.BuildLegs();
         _enemyBuilder.BuildWeapon();
      }
   }
}
