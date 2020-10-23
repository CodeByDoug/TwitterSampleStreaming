using System.Collections.Generic;

namespace Dcm.Twitter.Business.Contract
{
    public interface IEmojiManager
    {
        void Process(string message);

        bool DoesTweetContainsEmojis { get; set; }

        IEnumerable<string> GetMostPopularEmojis(int count);

        void Reset();
    }
}