using Dcm.Twitter.Entity;
using System.Collections.Generic;

namespace Dcm.Twitter.Business.Contract
{
    public interface IUrlManager
    {
        void Process(IEnumerable<Urls> Urls);

        bool DoesTweetContainUrls { get; set; }

        IEnumerable<string> GetMostPopularUrls(int count);

        void Reset();
    }
}