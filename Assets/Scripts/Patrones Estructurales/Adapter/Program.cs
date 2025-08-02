using UnityEngine;

namespace Patrones_Estructurales.Adapter
{
    public class Program : MonoBehaviour
    {
        //El programa recoje el objeto que no está adaptado y le envia al adaptaador su clase 
        private void Start()
        {
            var groundEnemy = gameObject.AddComponent<GroundEnemy>();
            IAdaptable target = new EnemyAdapter(groundEnemy);
            
            //Despues de adaptarla se puede usar
            Debug.Log(target.GetPosition());
        }
    }
}
