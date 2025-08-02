using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Services.GenericProviderService
{
    public abstract class GenericProviderService<T, TK> : BaseService where TK : GenericElement<T> where T : struct
    {
        public GenericScriptableObject<T,TK> provider;

        /// <summary>
        /// The key must be a struct used to compare values inside the IEquipmentComparable implemented by GenericEquipment
        /// Returns a Generic equipment, that can be caset to monobehaviour, IGenericEquippable or IEquipmentComparable
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual TK GetElement(T key) => provider.elements.Find(k => k.Compare(key));
        
        public virtual TK GetElement(Predicate<T> comparisionPredicate) => provider.elements.Find(k => k.Compare(comparisionPredicate));

        public virtual TK GetRandomElement() => provider.GetRandom();

        public virtual TK InstantiateElement(T key, Transform parent) => Object.Instantiate(GetElement(key), parent);

        public virtual TK1 InstantiateElement<TK1>(T key, Transform parent) where TK1 : GenericElement<T> 
            => Object.Instantiate(GetElement(key), parent) as TK1;
        
        public virtual TK1 InstantiateElement<TK1>(T key, Transform parent, Vector3 pos) where TK1 : GenericElement<T>
            => Object.Instantiate(GetElement(key), pos, Quaternion.identity, parent) as TK1;
    }
}