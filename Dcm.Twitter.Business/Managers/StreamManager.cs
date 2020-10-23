using Dcm.Twitter.Business.Contract;
using Dcm.Twitter.Contracts;
using Dcm.Twitter.Entity;
using Dcm.Twitter.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Dcm.Business.Manager
{
    public class StreamManager : IStreamManager
    {
        private readonly ITweetManager _tweetManager;
        private readonly ITweetCalculationEngine _tweetCalculationEngine;
        private IStreamDetails _streamDetails;

        private ConcurrentQueue<Tweet> _queue;
        private HttpClient _httpClient;

        public StreamManager(ITweetManager tweetManager, IEmojiManager emojiManager, IHashtagManager hashtagManager, IUrlManager urlsManager, ITweetCalculationEngine tweetCalculationEngine, IStreamDetails streamDetails)
        {
            _queue = new ConcurrentQueue<Tweet>();
            _tweetManager = tweetManager;
            _tweetCalculationEngine = tweetCalculationEngine;
            _streamDetails = streamDetails;

            _httpClient = HttpClientFactory.Create();
        }

        public async Task StartTweetStreamingAsync()
        {
            _tweetManager.Reset();
            _streamDetails.StartTime = DateTime.Now;
            _streamDetails.StopTime = null;
            GetTweetsAsync();
            ProcessTweetsAsync();
        }

        public async Task<IStreamDetails> StartTweetStreamingAsync(int minutes)
        {
            _tweetManager.Reset();
            _streamDetails.StartTime = DateTime.Now;
            _streamDetails.StopTime = _streamDetails.StartTime.AddMinutes(minutes);
            GetTweetsAsync();
             ProcessTweets();

            return GetStreamStats();
        }

        public async Task<IStreamDetails> StopTweetStreamingAsync()
        {
            _streamDetails.StopTime = DateTime.Now;

            return GetStreamStats();
        }

        private async Task GetTweetsAsync()
        {
            var token = "AAAAAAAAAAAAAAAAAAAAACnkIwEAAAAAsDKaaXT4cUw9YaR8rFEo07zI1dM%3DhefB8fMR5plD9aisb2I84YeB8yACDb5yLFOkNABxXW6yA7ic5A";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var stream = await _httpClient.GetStreamAsync(TwitterEndpoints.GetSampledTweets).ConfigureAwait(false);

            using (var reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream && (!_streamDetails.StopTime.HasValue || _streamDetails.StopTime > DateTime.Now))
                {
                    _queue.Enqueue(JsonConvert.DeserializeObject<Tweet>(reader.ReadLine()));
                }
            }
        }

        //private async Task ProcessTweetsAsync()
        //{
        //    var tweetProcessor = new Thread(() =>
        //  {
        //      while (!_streamDetails.StopTime.HasValue || _streamDetails.StopTime > DateTime.Now)
        //      {
        //          if (!_queue.IsEmpty && _queue.TryDequeue(out var tweet) && tweet != null)
        //          {
        //              _tweetManager.ProcessTweet(tweet);
        //          }
        //      }
        //  });

        //    tweetProcessor.Start();
        //}

        private async Task ProcessTweetsAsync()
        {
            //var tweetProcessor = new Thread(() => ProcessTweets());

            //tweetProcessor.Start();


            new Thread(() => ProcessTweets()).Start();
        }

        private void ProcessTweets()
        {
            while (!_streamDetails.StopTime.HasValue || _streamDetails.StopTime > DateTime.Now)
            {
                if (!_queue.IsEmpty && _queue.TryDequeue(out var tweet) && tweet != null)
                {
                    _tweetManager.ProcessTweet(tweet);
                }
            }
        }

        public IStreamDetails GetStreamStats()
        {
            return _tweetCalculationEngine.CalculateStreamData();
        }
    }
}