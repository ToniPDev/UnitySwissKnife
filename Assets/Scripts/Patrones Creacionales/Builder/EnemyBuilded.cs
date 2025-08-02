using System.Collections.Generic;
using UnityEngine;

namespace Patrones_Creacionales.Builder
{
    public class EnemyBuilded : MonoBehaviour
    {
        public List<GameObject> parts = new List<GameObject>();

        public void Add(GameObject part) => parts.Add(part);
    }
}
