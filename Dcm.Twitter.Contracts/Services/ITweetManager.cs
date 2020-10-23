using Dcm.Twitter.Entity;
using System.Collections.Generic;

namespace Dcm.Twitter.Business.Contract
{
    public interface ITweetManager
    {
        void ProcessTweet(Tweet tweet);

        void Reset();

        IEnumerable<string> GetMostPopularEmojis(int count);

        IEnumerable<string> GetMostPopularHashtags(int count);

        IEnumerable<string> GetMostPopularUrls(int count);
    }
}