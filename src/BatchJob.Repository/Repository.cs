using BatchJob.Domain;
using Migrant.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob.Repository
{
    public class Repository<T> : IRepository<T>
        where T : BaseEntity
    {
        public BatchJobDbContext DbContext { get; set; }

        public virtual T CreateEntity(T t)
        {
            t = DbContext.Set<T>().Add(t);
            DbContext.SaveChanges();
            return t;
        }

        public virtual void CreateEntities(IList<T> list)
        {
            DbContext.Set<T>().AddRange(list);
            DbContext.SaveChanges();
        }

        public virtual T ModifiedEntity(T t)
        {
            DbContext.SaveChanges();
            return t;
        }

        public virtual bool TryDelete(object id)
        {
            var t = Find(id);
            if (t != null)
            {
                DbContext.Set<T>().Remove(t);
                return true;
            }
            return false;
        }

        public virtual T Find(object id)
        {
            return DbContext.Set<T>().Find(id);
        }
    }
}
