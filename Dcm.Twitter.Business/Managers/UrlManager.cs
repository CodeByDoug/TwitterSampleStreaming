using Dcm.Twitter.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using Dcm.Twitter.Business.Contract;
using Dcm.Twitter.Entity;

namespace Dcm.Business.Manager
{
    public class UrlManager : IUrlManager
    {
        private readonly IUrlRepository _urlsRepository;

        public UrlManager(IUrlRepository urlsRepository)
        {
            _urlsRepository = urlsRepository;
        }

        public bool DoesTweetContainUrls { get; set; }

        public void Process(IEnumerable<Urls> Urls)
        {
            DoesTweetContainUrls = Urls.ToList().Count > 0;

            foreach (var url in Urls)
            {
                var uri = new Uri(url.Expanded_url ?? url.Url);
                _urlsRepository.AddProcessedItem(new Twitter.Models.Url { Domain = uri.Host });
            }
        }

        public IEnumerable<string> GetMostPopularUrls(int count)
        {
            return _urlsRepository.GetAllProcessedItems().ToList().GroupBy(p => p.Domain).OrderByDescending(p => p.Count()).Take(count).Select(p => p.Key).ToList();
        }

        public void Reset()
        {
            _urlsRepository.Clear();
        }
    }
}
