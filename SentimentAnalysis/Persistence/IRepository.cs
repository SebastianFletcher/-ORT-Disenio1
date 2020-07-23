using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Persistence
{
    public interface IRepository<T>
    {
        void Add(T toAdd);

        void Delete(int? id);

        void Modify(int? id, T toModify);

        bool IsEmpty();

        T Get(int id);

        void Exists(T toFind);

        IEnumerable<T> GetAll();

        void Clear();

    }
}
