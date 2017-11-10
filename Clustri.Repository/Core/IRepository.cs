using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Clustri.Repository.Core
{
    public interface IRepository<TNode> where TNode : class
    {
        void Add(TNode node);

        void Remove(int id);

        IEnumerable<TNode> All();
        bool Contains(int id);
    }
}
