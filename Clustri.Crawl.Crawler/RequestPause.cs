using System.Threading.Tasks;
using Clustri.Crawl.Crawler.Interfaces;

namespace Clustri.Crawl.Crawler
{
    public class RequestPause : IPause
    {
        private readonly int _delay;

        public RequestPause(int delay)
        {
            _delay = delay;
        }

        public void Pause()
        {
            Task.Delay(_delay).Wait();
        }
    }
}
