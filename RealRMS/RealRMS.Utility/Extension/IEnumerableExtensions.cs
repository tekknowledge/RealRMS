using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RealRMS.Utility.Extension
{
    public static class IEnumerableExtensions {
        public static bool IsEqualTo<T>(this IEnumerable<T> col, IEnumerable<T> col2) {
            return !col.Except(col2).Any();
        }
    }
}
