using UnityEngine;

namespace Patrones_Creacionales.Factory_Method
{
    //Los creadores concretos overridean la funcion principal de los creador para devolver typos personalizados de obj
    public class NpcAffinityNpcCreator : NpcCreator
    {
        //Overridea la funcion de crear obj para devolver el objeto deseado
        protected override INpcProduct FactoryMethod(GameObject gameObject)
        {
            return gameObject.AddComponent<AffinityNpc>();
        }
    }
}
