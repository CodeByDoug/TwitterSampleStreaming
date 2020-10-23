using Dcm.Twitter.Repository.Contract;
using Dcm.Twitter.Models;
using System.Collections.Generic;

namespace Dcm.Twitter.Repository
{
    public class HashtagRepository : IHashtagRepository
    {
        private readonly List<Hashtag> _hashtags = new List<Hashtag>();

        public void AddProcessedItem(Hashtag hashtag)
        {
            _hashtags.Add(hashtag);
        }

        public void Clear()
        {
            _hashtags.Clear();
        }

        public IEnumerable<Hashtag> GetAllProcessedItems()
        {
            return _hashtags;
        }

    }
}
