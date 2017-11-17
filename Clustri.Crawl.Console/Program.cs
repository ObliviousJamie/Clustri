using System;
using Clustri.Repository.Core;
using Clustri.Repository.Entities;

namespace Clustri.Crawl.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var ioc = new IocContainer();

            var unitOfWork = new UnitOfWorkFactory("http://localhost:7474/db/data", args[0], args[1]).Create();
            var userRepo = unitOfWork.Users;

            var mfc = new MfcCrawler(ioc, userRepo, new Printer(5), 500);

            var link = new Uri(@"http://steamcommunity.com/id/faucomte97");
            mfc.Crawl(100, link);
        }
    }
}
