using UnityEngine;

namespace Patrones_Creacionales.Builder
{
    //Cada builder concreto implementa todas o algunas de las Partes del builder
    public class ElfBuilder : IEnemyBuilder
    {
        private readonly EnemyBuilded _enemy;

        public ElfBuilder()
        {
            var enemy = new GameObject("Enemy");
            _enemy = enemy.AddComponent<EnemyBuilded>();
        }

        public void BuildHead()
        {
            var head = GameObject.CreatePrimitive(PrimitiveType.Cube);
            head.gameObject.name = "Elf head";
            head.transform.position = new Vector3(0, 1, 0);
            head.GetComponent<MeshRenderer>().material.color = Color.blue;
            head.transform.SetParent(_enemy.transform);
            _enemy.Add(head);
        }

        public void BuildBody()
        {
            var body = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            body.gameObject.name = "Elf body";
            body.transform.position = new Vector3(0, 0, 0);
            body.GetComponent<MeshRenderer>().material.color = Color.blue;
            body.transform.SetParent(_enemy.transform);
            _enemy.Add(body);
        }

        public void BuildLegs()
        {
            var legs = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            legs.gameObject.name = "Elf legs";
            legs.transform.position = new Vector3(0, -1, 0);
            legs.GetComponent<MeshRenderer>().material.color = Color.blue;
            legs.transform.SetParent(_enemy.transform);
            _enemy.Add(legs);
        }

        public void BuildWeapon()
        {
            var weapon = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            weapon.gameObject.name = "Elf weapon";
            weapon.transform.position = new Vector3(2, 0, 0);
            weapon.GetComponent<MeshRenderer>().material.color = Color.red;
            weapon.transform.SetParent(_enemy.transform);
            _enemy.Add(weapon);
        }

        public EnemyBuilded GetEnemy() => _enemy;
    }
}
