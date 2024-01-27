using System;
using System.Collections.Generic;

namespace DH.Core.Util
{
    public static class CommonExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (T item in items)
                action(item);
        }
    }
}