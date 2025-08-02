using UnityEngine;

namespace Patrones_Creacionales.Factory_Method
{
    public class InfoNpc : MonoBehaviour,INpcProduct
    {
        public string NpcType() => "Informacion";

        public void BuildNpc(Mesh mesh)
        {
            mesh.Clear();
            mesh.vertices = _vertices;
            mesh.triangles = _triangles;
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            mesh.Optimize();
        }

        public void OnMouseDown()
        {
            Debug.Log(NpcType());
        }

        Vector3[] _vertices = 
        {
            new Vector3 (0, 0, 0),
            new Vector3 (1, 0, 0),
            new Vector3 (0.5f, 1, 0.5f),
            new Vector3 (1, 0, 1),
            new Vector3 (0, 0, 1),
        };

        readonly int[] _triangles = 
        {
            0, 1, 2, 
            0, 4, 2,
            2, 4, 3,
            2, 1, 3,
            0, 3, 1,
            0, 4, 3,
            2,4,0,
            2,1,3
        };
    }
}
