using UnityEngine;

namespace Patrones_Creacionales.Builder
{
    public class OrcBuilder : IEnemyBuilder
    {
        private readonly EnemyBuilded _enemy;

        public OrcBuilder()
        {
            var enemy = new GameObject("Enemy");
            _enemy = enemy.AddComponent<EnemyBuilded>();
        }
        

        public void BuildHead()
        {
            var head = GameObject.CreatePrimitive(PrimitiveType.Cube);
            head.gameObject.name = "Orc head";
            head.transform.position = new Vector3(0, 1, 0);
            head.GetComponent<MeshRenderer>().material.color = Color.green;
            head.transform.SetParent(_enemy.transform);
            _enemy.Add(head);
        }

        public void BuildBody()
        {
            var body = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            body.gameObject.name = "Orc body";
            body.transform.position = new Vector3(0, 0, 0);
            body.GetComponent<MeshRenderer>().material.color = Color.green;
            body.transform.SetParent(_enemy.transform);
            _enemy.Add(body);
        }

        public void BuildLegs()
        {
            var legs = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            legs.gameObject.name = "Orc legs";
            legs.transform.position = new Vector3(0, -1, 0);
            legs.GetComponent<MeshRenderer>().material.color = Color.green;
            legs.transform.SetParent(_enemy.transform);
            _enemy.Add(legs);
        }

        public void BuildWeapon()
        {
        }

        public EnemyBuilded GetEnemy() => _enemy;
    }
}
