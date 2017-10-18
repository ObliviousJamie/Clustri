using System;
using System.Collections.Generic;
using System.Text;

namespace Clustri.Crawl.Crawler.Interfaces
{
    public interface IScheduler : IEnumerable<IVertex>
    {
        void Add(Tuple<IExploredVertex, IEnumerable<IUnexploredVertex>> tuple);
    }
}
