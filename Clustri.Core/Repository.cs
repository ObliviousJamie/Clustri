using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Clustri.Core
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public virtual void Add(TEntity entity)
        {

        }

        public void Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity Get(TEntity id)
        {
            throw new NotImplementedException();
        }

        public TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
