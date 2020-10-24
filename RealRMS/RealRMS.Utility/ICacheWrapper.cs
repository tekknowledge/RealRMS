namespace RealRMS.Utility {
    public interface ICacheWrapper {
        object Get(string key);

        void Set(string key, object value);

        void Remove(string key);
    }
}
