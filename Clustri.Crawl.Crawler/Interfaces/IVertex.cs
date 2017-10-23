using System;
using System.Collections.Generic;

namespace Clustri.Crawl.Crawler.Interfaces
{
    public interface IVertex
    {
        int Degree { get; }
        string Id { get; }
        IEnumerable<IProfile> Degrees { get;  }
        int Weight { get; set; }
    }
}
