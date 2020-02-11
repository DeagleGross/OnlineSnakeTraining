using System;
using System.Collections.Generic;

namespace SafeboardSnake.Core.Helpers
{
    public static class LinkedListHelper
    {
        /// <summary>
        /// Returns (предпоследний) last but one element in linkedList.
        /// If passed linkedList doesn't have at least 2 elements throws argument exception.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T GetLastButOne<T>(this LinkedList<T> list)
        {
            if (list.Count > 1)
            {
                // ReSharper disable once PossibleNullReferenceException
                return list.Last.Previous.Value;
            }

            throw new ArgumentException($"List doesn't contain 2 elements. Can not get 'last but one' element.");
        }
    }
}
