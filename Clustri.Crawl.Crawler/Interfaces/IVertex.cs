using System;
using System.Collections.Generic;
using System.Text;

namespace Clustri.Crawl.Crawler.Interfaces
{
    public interface IVertex
    {
        int Degree { get; }
        string Name { get; }
    }
}
