using UnityEngine;

namespace Patrones_Estructurales.Adapter
{
    //Objetivo con una interfaz incompatible con GroundEnemy
    public class FlyingEnemy : MonoBehaviour,IAdaptable
    {
        //Funcion de la interfaz
        public string GetPosition()
        {
            return " over 9000";
        }
    }

    public interface IAdaptable
    {
        string GetPosition();
    }
}
