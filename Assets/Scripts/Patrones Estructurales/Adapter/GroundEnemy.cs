using UnityEngine;

namespace Patrones_Estructurales.Adapter
{
    //Clase que ha de ser adaptada
    public class GroundEnemy : MonoBehaviour
    {
        //Funcion propia
        public string GetSpecificPosition()
        {
            return "Specific position is ";
        }
    }
}
