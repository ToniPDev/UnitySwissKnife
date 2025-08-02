using System;

namespace Services.GenericProviderService
{
    public interface IGenericElementComparer<T> where T : struct
    {
        public bool Compare(T valueToCompare);
        public bool Compare(Predicate<T> valueToCompare);
    }
}