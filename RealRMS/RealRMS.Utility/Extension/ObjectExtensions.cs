using System;

namespace RealRMS.Utility.Extension {
    public static class ObjectExtensions {
        public static string SafeToString(this object o) {
            if (o == DBNull.Value)
                return null;
            if (o == null)
                return null;
            return o.ToString();
        }

        public static int SafeToInt(this object o) {
            /*int returnValue = 0;
            if (o == DBNull.Value)
                return returnValue;
            if (o == null)
                return returnValue;
            string str = o.SafeToString();
            if (str == null)
                return returnValue;
            int.TryParse(str, out returnValue);

            return returnValue;*/
            if (o == DBNull.Value)
                return default(int);
            if (o == null)
                return default(int);
            return Convert.ToInt32(o);
        }

        public static bool SafeToBool(this object o) {
            if (o == DBNull.Value)
                return false;
            if (o == null)
                return false;
            return Convert.ToBoolean(o);
        }
    }
}
