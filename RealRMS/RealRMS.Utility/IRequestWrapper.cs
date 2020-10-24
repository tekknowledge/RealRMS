namespace RealRMS.Utility {
    public interface IRequestWrapper {
        string QueryString(string key);

        string Form(string key);
    }
}