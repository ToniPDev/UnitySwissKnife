using UnityEngine;
using UnityEngine.EventSystems;

namespace Patrones_Creacionales.Factory_Method
{
    //Clases que implementan las funciones del producto
    public class AffinityNpc : MonoBehaviour,INpcProduct
    {
        public string NpcType() => "Afinidad";

        public void BuildNpc(Mesh mesh)
        {
            mesh.Clear();
            mesh.vertices = _vertices;
            mesh.triangles = _triangles;
            mesh.Optimize();
            mesh.RecalculateNormals();
        }

        public void OnMouseDown()
        {
            Debug.Log(NpcType());
        }

        Vector3[] _vertices = 
        {
            new Vector3 (0, 0, 0),
            new Vector3 (1, 0, 0),
            new Vector3 (1, 1, 0),
            new Vector3 (0, 1, 0),
            new Vector3 (0, 1, 1),
            new Vector3 (1, 1, 1),
            new Vector3 (1, 0, 1),
            new Vector3 (0, 0, 1),
        };

        readonly int[] _triangles = 
        {
            0, 2, 1, //face front
            0, 3, 2,
            2, 3, 4, //face top
            2, 4, 5,
            1, 2, 5, //face right
            1, 5, 6,
            0, 7, 4, //face left
            0, 4, 3,
            5, 4, 7, //face back
            5, 7, 6,
            0, 6, 7, //face bottom
            0, 1, 6 
        };

        public void OnPointerDown(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }
    }
}
