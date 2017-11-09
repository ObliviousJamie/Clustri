using System;
using System.Linq.Expressions;

namespace Clustri.Core
{
    interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);

        void Remove(TEntity entity);

        TEntity Get(TEntity id);
        TEntity Find(Expression<Func<TEntity, bool>> predicate);
    }
}
