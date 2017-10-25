using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Clustri.Crawl.Crawler.Interfaces;

namespace Clustri.Crawl.Crawler
{
    /// <summary>
    /// Scheduler based on mutual friend crawling
    /// </summary>
    public class MfcScheduler : IScheduler
    {
        private readonly ILogSet<IVertex> _visited;
        private readonly IDictionary<IVertex, double> _queue;

        public MfcScheduler(ILogSet<IVertex> visited, IDictionary<IVertex, double> queue)
        {
            _visited = visited;
            _queue = queue;
        }


        public IEnumerator<IVertex> GetEnumerator()
        {
            var next = _queue.Aggregate((first, second) => first.Value > second.Value ? first : second).Key;
            yield return next;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(IVertex vertex)
        {
            var unexplored = !_visited.Exists(vertex);

            //Private profile or no friends - do not explore
            if(vertex.Degree == 0)
                _visited.Add(vertex);

            if (unexplored && vertex.Degree > 0)
                UpdateReferenceScore(vertex);
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
            _visited.Add(vertex);
            _queue.Remove(vertex);
        }

        private void UpdateReferenceScore(IVertex vertex)
        {
            double curRefScore;
            var present = _queue.TryGetValue(vertex, out curRefScore);

            var oneReference = 1.0 / vertex.Degree;

            if (present)
                curRefScore = curRefScore + oneReference;
            else
                curRefScore = oneReference;

            _queue[vertex] = curRefScore;
        }
    }
}
