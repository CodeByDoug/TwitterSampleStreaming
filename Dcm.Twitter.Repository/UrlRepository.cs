using Dcm.Twitter.Repository.Contract;
using Dcm.Twitter.Models;
using System.Collections.Generic;

namespace Dcm.Twitter.Repository
{
    public class UrlRepository : IUrlRepository
    {
        private readonly List<Url> _urls = new List<Url>();

        public void AddProcessedItem(Url url)
        {
            _urls.Add(url);
        }

        public void Clear()
        {
            _urls.Clear();
        }

        public IEnumerable<Url> GetAllProcessedItems()
        {
            return _urls;
        }
    }
}
