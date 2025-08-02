using UnityEngine;

namespace Patrones_Creacionales.Abstract_Factory
{
    //La interfaz declara un grupo de metodos que crean los diferentes tipos de productos(npc y jugadores)
    public interface IAbstractFactory
    {
        IAbstractNpc CreateNpc(GameObject gameObject);

        IAbstractEnemy CreateEnemy(GameObject gameObject);
    }
}
