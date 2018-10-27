using System;
using System.Collections.Generic;
using System.Text;

namespace Migrant.Domain
{
    public interface IRepository<T>
    {
        T Find(object id);

        T CreateEntity(T t);

        void CreateEntities(IList<T> list);

        T ModifiedEntity(T t);

        bool TryDelete(object id);
    }
}
