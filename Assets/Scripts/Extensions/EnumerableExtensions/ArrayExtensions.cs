using System;
using Systems.ReferenceResolver;
using Object = UnityEngine.Object;

namespace Extensions.EnumerableExtensions
{
    public static class ArrayExtensions
    {
        public static T ReferenciableBinarySearch<T>(this IReferenciable[] enumerable, int start, int end) where T : IReferenciable
        {
            // Si el índice start es mayor al end, acabamos de buscar
            if (start > end) return default;
            
            // Obtenemos el índice medio
            var middle = (start + end) / 2;
            
            // Si el objeto está en el medio lo devolvemos
            if (enumerable[middle] is T) return (T)enumerable[middle];

            // Buscamos de la mitad a la izquierda
            var searchToLeft = ReferenciableBinarySearch<T>(enumerable, start, middle - 1);
            
            // Si lo ha encontrado lo devolvemos
            if (searchToLeft != null) return searchToLeft;

            // Si no lo encuentra, busca hacia la derecha
            var searchToRight = ReferenciableBinarySearch<T>(enumerable, middle + 1, end);
            
            return searchToRight;
        }
        
        public static Object BinaryFindElement<T>(this Object[] enumerable,int start, int end) where T : Object
        {
            //Si el indice start es mayor al end acabamos de buscar
            if (start > end) return null;
            
            //Obtenemos el indice medio
            var middle = (int)Math.Floor( (start+end) /2f );
            
            //Si el objeto esta al medio lo devolvemos
            if(enumerable[middle].GetType() == typeof(T)) return enumerable[middle];

            //Buscamos de la mitad a izquierda
            var searchToLeft = BinaryFindElement<T>(enumerable, start, middle - 1);
            
            //Si lo ha encontrado lo devuelve
            if (searchToLeft != null) return searchToLeft;

            //Si no lo encuentra busca hacia la derecha
            var searchToRight =  BinaryFindElement<T>(enumerable, middle + 1, end);
            
            return searchToRight != null ? searchToRight : null;
        }
    }
}