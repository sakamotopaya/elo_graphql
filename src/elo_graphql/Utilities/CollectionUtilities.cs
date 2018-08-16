using System;
using System.Collections.Generic;

namespace Elo
{
    public static class CollectionUtilities
    {
        public static void Each<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (T item in items)
            {
                action(item);
            }
        }
    }
}