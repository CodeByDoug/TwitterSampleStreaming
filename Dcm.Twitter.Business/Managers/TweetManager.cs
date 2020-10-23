using Dcm.Twitter.Models;
using Dcm.Twitter.Repository.Contract;
using System.Collections.Generic;
using Dcm.Twitter.Business.Contract;
using Dcm.Twitter.Entity;

namespace Dcm.Business.Manager
{
    public class TweetManager : ITweetManager
    {
        private readonly IMediaManager _mediaManager;
        private readonly IEmojiManager _emojiManager;
        private readonly IHashtagManager _hashtagManager;
        private readonly IUrlManager _urlManager;
        private readonly ITweetRepository _tweetRepository;

        public TweetManager(ITweetRepository tweetRepository, IEmojiManager emojiManager, IHashtagManager hashtagManager, IUrlManager urlsManager, IMediaManager mediaManager)
        {
            _emojiManager = emojiManager;
            _tweetRepository = tweetRepository;
            _hashtagManager = hashtagManager;
            _urlManager = urlsManager;
            _mediaManager = mediaManager;
        }

        public void ProcessTweet(Tweet tweetEntity)
        {
            var users = tweetEntity.Includes?.Users;
            var media = tweetEntity.Includes?.Media;

            var hashtags = GetHashtags(users);
            var urls = GetUrls(users);

            _emojiManager.Process(tweetEntity.Data.Text);
            _hashtagManager.Process(hashtags);
            _urlManager.Process(urls);
            _mediaManager.Process(media);

            _tweetRepository.AddProcessedTweet(new ProcessedTweet
            {
                DoesTweetContainAnyEmojis = _emojiManager.DoesTweetContainsEmojis,
                DoesTweetContainAnyHashtags = _hashtagManager.DoesTweetContainHashtags,
                DoesTweetContainAnyUrls = _urlManager.DoesTweetContainUrls,
                DoesTweetContainAnyMedia = _mediaManager.DoesTweetContainMedia
            });
        }

        private List<Hashtags> GetHashtags(List<Users> users)
        {
            var hashtags = new List<Hashtags>();
            foreach (var user in users)
            {
                var userHashtags = user.Entities?.Description?.Hashtags;
                if (userHashtags != null)
                {
                    hashtags.AddRange(userHashtags);
                }
            }
            return hashtags;
        }
      
        private List<Urls> GetUrls(List<Users> users)
        {
            var urls = new List<Urls>();
            foreach (var user in users)
            {
                var userUrls = user.Entities?.Url?.Urls;
                if (userUrls != null)
                {
                    urls.AddRange(userUrls);
                }
            }
            return urls;
        }

        public IEnumerable<string> GetMostPopularEmojis(int count)
        {
            return _emojiManager.GetMostPopularEmojis(count);
        }

        public IEnumerable<string> GetMostPopularHashtags(int count)
        {
            return _hashtagManager.GetMostPopularHashtags(count);
        }

        public IEnumerable<string> GetMostPopularUrls(int count)
        {
            return _urlManager.GetMostPopularUrls(count);
        }

        public void Reset()
        {
            _tweetRepository.Clear();
            _emojiManager.Reset();
            _hashtagManager.Reset();
            _urlManager.Reset();
        }
    }
}