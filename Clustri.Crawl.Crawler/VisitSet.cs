using System.Collections.Generic;
using Clustri.Crawl.Crawler.Interfaces;

namespace Clustri.Crawl.Crawler
{
    public class VisitSet<T> : ILogSet<T>
    {
        private readonly ICollection<T> _collection;

        public VisitSet()
        {
           _collection = new HashSet<T>(); 
        }

        public bool Exists(T item)
        {
            return _collection.Contains(item);
        }

        public void Add(T item)
        {
            _collection.Add(item);
        }
    }
}
