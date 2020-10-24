using System;
using System.Reflection;
using System.Linq;

namespace RealRMS.Utility.Extension {
    public static class MemberInfoExtensions {
        public const string MULTIPLE_ATTRIBUTE_ERROR = "Multiple attributes declared";

        public static bool HasAttribute(this MemberInfo info, Type type) {
            return info.GetCustomAttributes(type, true).Length > 0;
        }

        public static T GetAttribute<T>(this MemberInfo info) {
            object[] targetAttributes = info.GetCustomAttributes(typeof(T), true);

            if (targetAttributes.Length == 1)
                return ((T)targetAttributes[0]);
            return default(T);
        }

    }
}
