using System;
using System.Collections.Generic;

namespace Dcm.Twitter.Contracts
{
    public interface IStreamDetails
    {
        DateTime StartTime { get; set; }

        DateTime? StopTime { get; set; }

        TimeSpan ExecutionTime { get; set; }

        double TotalNumberOfTweets { get; set; }

        double NumberOfTweetsPerHour { get;  }

        double NumberOfTweetsPerMinute { get;  }

        double NumberOfTweetsPerSecond { get;  }

        IEnumerable<string> MostPopularEmojis { get; set; }

        IEnumerable<string> MostPopularHashtags { get; set; }

        IEnumerable<string> MostPopularUrls { get; set; }

        double PercentageOfTweetsThatContainEmojis { get; set; }

        double PercentageOfTweetsThatContainUrls { get; set; }

        double PercentageOfTweetsThatContainMedia { get; set; }
    }
}
