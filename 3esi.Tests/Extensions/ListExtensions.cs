using System.Collections.Generic;

namespace _3esi.Tests.Extensions
{
    public static class ListExtensions
    {
        public static bool IsNullOrEmpty<T>(this List<T> listToCheck)
        {
            return listToCheck.IsNull() || listToCheck.Count <= 0;
        }

        public static bool IsNullOrEmpty<T>(this IList<T> listToCheck)
        {
            return listToCheck.IsNull() || listToCheck.Count <= 0;
        }
        public static bool IsNull(this object objectToCheck)
        {
            return objectToCheck == null;
        }
    }
}