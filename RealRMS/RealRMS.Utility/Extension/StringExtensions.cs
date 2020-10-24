namespace RealRMS.Utility.Extension
{
    public static class StringExtensions {
        public static bool EqualsCaseInsensitive(this string str, string compareTo) {
            if (str == null && compareTo == null)
                return true;
            if (str == null)
                return false;
            if (compareTo == null)
                return false;
            if (str.ToLower() == compareTo.ToLower())
                return true;
            return false;
        }
    }
}
