using Dcm.Twitter.Repository.Contract;
using Dcm.Twitter.Models;
using System.Collections.Generic;
using System.Linq;
using Dcm.Twitter.Business.Contract;
using Dcm.Twitter.Entity;

namespace Dcm.Business.Manager
{
    public class HashtagManager : IHashtagManager
    {
        private readonly IHashtagRepository _hashtagRepository;

        public bool DoesTweetContainHashtags { get; set; }

        public HashtagManager(IHashtagRepository hashtagRepository)
        {
            _hashtagRepository = hashtagRepository;
        }

        public void Process(List<Hashtags> hashtags)
        {
            DoesTweetContainHashtags = hashtags?.Count() > 0;

            foreach (var hashtag in hashtags)
            {
                _hashtagRepository.AddProcessedItem(new Hashtag { Text = hashtag.Tag });
            }
        }

        public IEnumerable<string> GetMostPopularHashtags(int count)
        {
            return _hashtagRepository.GetAllProcessedItems().ToList().GroupBy(p => p.Text).OrderByDescending(p => p.Count()).Take(count).Select(p => p.Key).ToList();
        }

        public void Reset()
        {
            _hashtagRepository.Clear();
        }
    }
}