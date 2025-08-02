using UnityEngine;

namespace Patrones_Creacionales.Abstract_Factory
{
    public class EnemyArmed : MonoBehaviour,IAbstractEnemy
    {
        public void Initialize()
        {
            Debug.Log(EnemyType());
            ChangeColor(Color.red);
            Move();
        }
        public string EnemyType() => "Soy un Enemigo armado";
        public void ChangeColor(Color color) => gameObject.GetComponent<MeshRenderer>().material.color = color;
        public void Destroy() => Destroy(gameObject);
        public void Move() => transform.position = new Vector3(2, 0, 0);
    }
    
    public class DisarmedEnemy : MonoBehaviour,IAbstractEnemy
    {
        public void Initialize()
        {
            Debug.Log(EnemyType());
            ChangeColor(Color.green);
            Move();
        }
        public string EnemyType() => "Soy un Enemigo desarmado";
        public void ChangeColor(Color color) => gameObject.GetComponent<MeshRenderer>().material.color = color;
        public void Destroy() => Destroy(gameObject);
        
        public void Move() => transform.position = new Vector3(2, 0, 0);
    }
    
    //cada producto de una familia ha de implementar una misma interfaz
    public interface IAbstractEnemy
    {
        string EnemyType();
        void ChangeColor(Color color);
        void Destroy();
        void Move();
        void Initialize();
    }
}
