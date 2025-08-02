using UnityEngine;

namespace Patrones_Creacionales.Factory_Method
{
    public class NpcInfoNpcCreator : NpcCreator
    {
        protected override INpcProduct FactoryMethod(GameObject gameObject)
        {
            return gameObject.AddComponent<InfoNpc>();
        }
    }
}
