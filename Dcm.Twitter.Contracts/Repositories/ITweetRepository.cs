using Dcm.Twitter.Models;
using System.Collections.Generic;

namespace Dcm.Twitter.Repository.Contract
{
    public interface ITweetRepository
    {
        void AddProcessedTweet(ProcessedTweet tweet);

        void Clear();

        List<ProcessedTweet> GetAllProcessedTweets();
    }
}
