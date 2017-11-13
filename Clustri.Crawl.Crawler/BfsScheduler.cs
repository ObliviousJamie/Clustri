using System.Collections;
using System.Collections.Generic;
using Clustri.Crawl.Crawler.Interfaces;

namespace Clustri.Crawl.Crawler
{
    public class BfsScheduler : IScheduler
    {
        private readonly ILogSet<IVertex> _visited;
        private readonly Queue<IVertex> _queue;

        public BfsScheduler(ILogSet<IVertex> visited, Queue<IVertex> queue)
        {
            _visited = visited;
            _queue = queue;
        }

        public IEnumerator<IVertex> GetEnumerator()
        {
            var vertex = _queue.Peek();
            yield return vertex;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(IVertex vertex)
        {
            var unexplored = !_visited.Exists(vertex);

            //Private profile or no friends - do not explore
            if (vertex.Degree == 0)
                _visited.Add(vertex);

            if (unexplored && vertex.Degree > 0)
                _queue.Enqueue(vertex);
        }

        public void Add(IEnumerable<IVertex> vertices)
        {
            foreach (var vertex in vertices)
            {
                Add(vertex);
            }
        }

        public void Remove(IVertex vertex)
        {
            _queue.Dequeue();
        }
    }
}
