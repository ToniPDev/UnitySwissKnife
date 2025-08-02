using UnityEngine;

namespace Patrones_Creacionales.Abstract_Factory
{
    //Los tipos de Npcs de una misma familia con caracteristicas diferentes
    public class NpcArmed : MonoBehaviour,IAbstractNpc
    {
        private IAbstractEnemy _enemy;
        public void Initialize(IAbstractEnemy enemyCollaborator)
        {
            ChangeColor(Color.red);
            Debug.Log(NpcType());
            SetEnemy(enemyCollaborator);
        }
        public void ChangeColor(Color color) => gameObject.GetComponent<MeshRenderer>().material.color = color;

        public string NpcType() => "Soy un Npc armado";

        public void SetEnemy(IAbstractEnemy enemyCollaborator) => _enemy = enemyCollaborator;

        public void OnMouseDown() => _enemy.Destroy();
    }
    
    public class DisarmedNpc : MonoBehaviour,IAbstractNpc
    {
        public void Initialize(IAbstractEnemy enemyCollaborator)
        {
            ChangeColor(Color.green);
            Debug.Log(NpcType());
            SetEnemy(enemyCollaborator);
        }
        public void ChangeColor(Color color) => gameObject.GetComponent<MeshRenderer>().material.color = color;
        
        private IAbstractEnemy _enemy;
        public string NpcType() => "Soy un Npc desarmado";
        public void SetEnemy(IAbstractEnemy enemyCollaborator) => _enemy = enemyCollaborator;
        public void OnMouseDown()
        {
            _enemy.ChangeColor(Color.cyan);
            Debug.Log("No puedo dispararle pero lo he asustado");
        }
    }

    //cada producto de una familia ha de implementar una misma interfaz
    //este producto es capaz de interactuar con los de otra interfaz
    public interface IAbstractNpc
    {
        void ChangeColor(Color color);
        void SetEnemy(IAbstractEnemy enemyCollaborator);
        string NpcType();
        void OnMouseDown();
        void Initialize(IAbstractEnemy enemyCollaborator);
    }
}