using Dcm.Twitter.Repository.Contract;
using Dcm.Twitter.Models;
using System.Collections.Generic;
using System.Linq;

namespace Dcm.Twitter.Repository
{
    public class TweetRepository : ITweetRepository
    {
        private readonly List<ProcessedTweet> _tweets = new List<ProcessedTweet>();

        public void AddProcessedTweet(ProcessedTweet tweet)
        {
            _tweets.Add(tweet);
        }

        public void Clear()
        {
            _tweets.Clear();
        }

        public List<ProcessedTweet> GetAllProcessedTweets()
        {
            return _tweets.ToList();
        }
    }
}
