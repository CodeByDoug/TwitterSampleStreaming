using Dcm.Twitter.Contracts;
using System;
using System.Collections.Generic;

namespace Dcm.Twitter.Models
{
    public class StreamDetails : IStreamDetails
    {
        public DateTime StartTime { get; set; }

        public DateTime? StopTime { get; set; }

        public TimeSpan ExecutionTime { get; set; }

        public double TotalNumberOfTweets { get; set; }

        public double NumberOfTweetsPerHour => Math.Round(TotalNumberOfTweets / ExecutionTime.TotalHours, MidpointRounding.AwayFromZero);

        public double NumberOfTweetsPerMinute => Math.Round(TotalNumberOfTweets / ExecutionTime.TotalMinutes, MidpointRounding.AwayFromZero);

        public double NumberOfTweetsPerSecond => Math.Round(TotalNumberOfTweets / ExecutionTime.TotalSeconds, MidpointRounding.AwayFromZero);

        public IEnumerable<string> MostPopularEmojis { get; set; }

        public IEnumerable<string> MostPopularHashtags { get; set; }

        public IEnumerable<string> MostPopularUrls { get; set; }

        public double PercentageOfTweetsThatContainEmojis { get; set; }

        public double PercentageOfTweetsThatContainUrls { get; set; }

        public double PercentageOfTweetsThatContainMedia { get; set; }
    }
}