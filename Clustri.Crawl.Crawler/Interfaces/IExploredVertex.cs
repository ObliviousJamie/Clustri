using System;
using System.Collections.Generic;
using System.Text;

namespace Clustri.Crawl.Crawler.Interfaces
{
    public interface IExploredVertex : IVertex
    {
        IEnumerable<IVertex> Edges { get;  }
        int Score { get;  }
    } 
}
