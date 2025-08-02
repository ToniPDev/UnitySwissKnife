using System;
using UnityEngine;

namespace Services.GenericProviderService
{
    public abstract class GenericElement<T> : MonoBehaviour,IGenericElement, IGenericElementComparer<T> where T : struct
    {
        protected IGenericElementComparer<T> GenericElementComparerImplementation;
        public virtual bool Compare(T valueToCompare) => GenericElementComparerImplementation.Compare(valueToCompare);
        public virtual bool Compare(Predicate<T> valueToCompare) => GenericElementComparerImplementation.Compare(valueToCompare);
    }
}