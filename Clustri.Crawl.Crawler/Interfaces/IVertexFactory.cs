﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Clustri.Crawl.Crawler.Interfaces
{
    public interface IVertexFactory
    {
        IVertex CreateNode();
    }
}