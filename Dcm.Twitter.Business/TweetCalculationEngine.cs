using Dcm.Twitter.Business.Contract;
using Dcm.Twitter.Repository.Contract;
using Dcm.Twitter.Contracts;
using System;
using System.Linq;

namespace Dcm.Twitter.Business
{
    public class TweetCalculationEngine : ITweetCalculationEngine
    {
        ITweetRepository _tweetRepository;
        IStreamDetails _streamDetails;
        ITweetManager _tweetManager;

        public TweetCalculationEngine(ITweetRepository tweetRepository, IStreamDetails streamDetails, ITweetManager tweetManager)
        {
            _streamDetails = streamDetails;
            _tweetRepository = tweetRepository;
            _tweetManager = tweetManager;
        }

        public IStreamDetails CalculateStreamData()
        {
            _streamDetails.ExecutionTime = DateTime.Now - _streamDetails.StartTime;

            var _processedTweets = _tweetRepository.GetAllProcessedTweets();

            var tweetsWithEmojisCount = _processedTweets.Count(t => t.DoesTweetContainAnyEmojis);
            var tweetsWithUrlsCount = _processedTweets.Count(t => t.DoesTweetContainAnyUrls);
            var tweetsWithMediaCount = _processedTweets.Count(t => t.DoesTweetContainAnyMedia);

            _streamDetails.TotalNumberOfTweets = _processedTweets.Count();

            _streamDetails.PercentageOfTweetsThatContainEmojis = (double)tweetsWithEmojisCount / _streamDetails.TotalNumberOfTweets;
            _streamDetails.PercentageOfTweetsThatContainUrls = (double)tweetsWithUrlsCount / _streamDetails.TotalNumberOfTweets;
            _streamDetails.PercentageOfTweetsThatContainMedia = (double)tweetsWithMediaCount / _streamDetails.TotalNumberOfTweets;

            _streamDetails.MostPopularEmojis = _tweetManager.GetMostPopularEmojis(10);
            _streamDetails.MostPopularHashtags = _tweetManager.GetMostPopularHashtags(10);
            _streamDetails.MostPopularUrls = _tweetManager.GetMostPopularUrls(10);

            return _streamDetails;
        }
    }
}