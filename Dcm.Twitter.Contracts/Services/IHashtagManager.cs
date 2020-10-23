using Dcm.Twitter.Entity;
using System.Collections.Generic;

namespace Dcm.Twitter.Business.Contract
{
    public interface IHashtagManager
    {
        void Process(List<Hashtags> hashtags);

        bool DoesTweetContainHashtags { get; set; }

        IEnumerable<string> GetMostPopularHashtags(int count);

        void Reset();
    }
}