using System;
using UnityEngine;

namespace Patrones_Estructurales.Adapter
{
    public class EnemyAdapter : IAdaptable
    {
        private readonly GroundEnemy _groundEnemy;
        
        //Se inizializa el adaptaador con la clase que queremos adaptar
        public EnemyAdapter(GroundEnemy enemy)
        {
            _groundEnemy = enemy;
        }
        
        public string GetPosition()
        {
            return _groundEnemy.GetSpecificPosition();
        }
    }
}
