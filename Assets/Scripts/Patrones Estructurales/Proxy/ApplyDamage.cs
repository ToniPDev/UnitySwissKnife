using System;
using UnityEngine;

namespace Patrones_Estructurales.Proxy
{
    public class ApplyDamage : MonoBehaviour
    {
        public bool isEnemy;
        private void Start()
        {
            DamageReceiver damageReceiver = new DamageReceiver();
            Proxy proxy = new Proxy(damageReceiver,isEnemy);
            
            ExecuteDamage(proxy,34);
        }

        private void ExecuteDamage(IProxyCommunicator communicator, int damage)
        {
            communicator.Request(damage);
        }
    }
}