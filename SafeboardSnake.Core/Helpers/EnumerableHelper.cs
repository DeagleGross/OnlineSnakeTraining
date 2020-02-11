using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SafeboardSnake.Core.Helpers
{
    public static class EnumerableHelper
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> ts)
        {
            if (ts == null)
            {
                return true;
            }

            return !ts.Any();
        }
    }
}
