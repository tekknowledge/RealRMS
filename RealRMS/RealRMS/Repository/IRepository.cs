using System.Collections.Generic;

namespace RealRMS.Repository {
    public interface IRepository<T> {
        bool Save(T model);

        T Get(int id);

        IEnumerable<T> GetAll();
    }
}