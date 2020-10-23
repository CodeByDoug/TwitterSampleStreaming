namespace Dcm.Twitter.Models
{
    public class ProcessedTweet
    {
        public bool DoesTweetContainAnyEmojis { get; set; }

        public bool DoesTweetContainAnyUrls { get; set; }

        public bool DoesTweetContainAnyMedia { get; set; }

        public bool DoesTweetContainAnyHashtags { get; set; }
    }
}