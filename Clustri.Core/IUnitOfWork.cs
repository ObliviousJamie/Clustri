using System;
using System.Collections.Generic;
using System.Text;

namespace Clustri.Core
{
    interface IUnitOfWork : IDisposable
    {
        bool Complete();
    }
}
