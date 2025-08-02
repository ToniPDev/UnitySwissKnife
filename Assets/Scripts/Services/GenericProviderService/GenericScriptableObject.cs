using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Services.GenericProviderService
{
    public class GenericScriptableObject<T,TK> : ScriptableObject where TK : GenericElement<T> where T : struct
    {
        public List<TK> elements;

        public TK GetRandom() => elements[new Random().Next(0, elements.Count)];
    }
}