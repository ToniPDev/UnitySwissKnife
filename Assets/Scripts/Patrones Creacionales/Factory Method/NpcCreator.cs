using UnityEngine;

namespace Patrones_Creacionales.Factory_Method
{
    // Declara método que devuelve un objeto creado, llamado por las subclases.
    
    public abstract class NpcCreator
    {
        //Método que se encarga de crear objeto.
        protected abstract INpcProduct FactoryMethod(GameObject gameObject);

        //Ejemplo de método de lógica interna que tiene la propia factoria, que contiene el comportamiento base del obj. 
        public void NpcInstantiator()
        {
            var newNpc = new GameObject();
            var npcFilter = newNpc.AddComponent<MeshFilter>();
            
            newNpc.AddComponent<MeshRenderer>();
            newNpc.AddComponent<BoxCollider>();

            var npc = FactoryMethod(newNpc);

            npc.BuildNpc(npcFilter.mesh);
        }
    }

    public interface INpcProduct
    {
        string NpcType();
        void BuildNpc(Mesh mesh);
        void OnMouseDown();
    }
}