using System.Collections.Generic;

namespace Clustri.Repository.Core.Interfaces
{
    public interface IRepository<TNode> where TNode : class
    {
        void Add(TNode node);

        void Remove(int id);

        IEnumerable<TNode> All();
        bool Contains(int id);
    }
}
