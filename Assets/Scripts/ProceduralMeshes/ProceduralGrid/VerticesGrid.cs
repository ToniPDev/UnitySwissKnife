using UnityEngine;

namespace ProceduralMeshes.ProceduralGrid
{
    [RequireComponent(typeof(MeshFilter)),RequireComponent(typeof(MeshRenderer))]
    public class VerticesGrid : MonoBehaviour
    {
        public int xSize, ySize;
        private Vector3[] _vertices;
        private Mesh _mesh;

        private void Awake()
        {
            GetReferences();
            GenerateMesh();
        }

        private void GetReferences()
        {
            GetComponent<MeshFilter>().mesh = _mesh = new Mesh();
            _mesh.name = "Procedural Grid";
        }

        //(x+1)(y+1) => Número de vértices
        private void GenerateMesh()
        {
            _vertices = new Vector3[(xSize + 1) * (ySize + 1)];
            var uv = new Vector2[_vertices.Length];
            var tangents = new Vector4[_vertices.Length];

            Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);
            
            for (int i = 0, y = 0; y <= ySize; y++)
            {
                for (var x = 0; x <= xSize; x++, i++)
                {
                    _vertices[i] = new Vector3(x, y);
                    uv[i] = new Vector2((float)x/xSize,(float)y/ySize);
                    tangents[i] = tangent;
                }
            }

            _mesh.vertices = _vertices;

            var triangles = new int[xSize * ySize * 6];
            for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++)
            {
                for (var x = 0; x < xSize; x++, ti += 6, vi++)
                {
                    triangles[ti] = vi;
                    triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                    triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                    triangles[ti + 5] = vi + xSize + 2;
                    
                }
            }
            _mesh.triangles = triangles;
            _mesh.RecalculateNormals();
            _mesh.uv = uv;
            _mesh.tangents = tangents;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            if (_vertices == null) return;
            foreach (var t in _vertices)
            {
                Gizmos.DrawSphere(t, 0.1f);
            }
        }
    }
}
