using System.Collections.Generic;

namespace Extensions.ListExtensions
{
    /// <summary>
    /// Generic list shuffle extension
    /// </summary>  
    public static class ListExtensions
    {
        public static List<T> ShuffleList<T>(this IEnumerable<T> listRef)
        {
            List<T> shuffledList = new(listRef);
            for (var i = 0; i < shuffledList.Count; i++)
            {
                var temp = shuffledList[i];
                var rand = UnityEngine.Random.Range(i, shuffledList.Count);
                shuffledList[i] = shuffledList[rand];
                shuffledList[rand] = temp;
            }
            return shuffledList;
        }
    }
}
