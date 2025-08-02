using UnityEngine;

namespace Patrones_Estructurales.Proxy
{
    public class Proxy : IProxyCommunicator
    {
        private readonly DamageReceiver _damageReceiver;
        private readonly bool _isEnemy;

        public Proxy(DamageReceiver damageReceiver,bool isEnemy)
        {
            _damageReceiver = damageReceiver;
            _isEnemy = isEnemy;
        }

        public void Request(int damage)
        {
            if (CheckEnemy())
            {
                Debug.Log("Enemy detected, sending damage to service");
                _damageReceiver.Request(damage);
            }
            else
            {
                Debug.Log("The objective is not an enemy, any damage will be applyed");
            }
        }

        private bool CheckEnemy() => _isEnemy;
    }
}