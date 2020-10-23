namespace Dcm.Twitter.Models
{
    public class TwitterEndpoints
    {
        public static string GetSampledTweets => "https://api.twitter.com/2/tweets/sample/stream?tweet.fields=created_at&expansions=author_id,attachments.media_keys&user.fields=created_at,entities";
    }
}
