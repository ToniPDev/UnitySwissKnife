using UnityEngine;

namespace Patrones_Estructurales.Proxy
{
    //El servicio tiene lógica dentro de el.
    public class DamageReceiver : IProxyCommunicator
    {
        private int _damageToApply;
        
        public void Request(int damage)
        {
            Debug.Log("Request Recieved, this ammount of damage will be applyed: " + damage);
            _damageToApply = damage;
        }
    }
}