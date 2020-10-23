using Dcm.Twitter.Entity;
using System.Collections.Generic;

namespace Dcm.Twitter.Business.Contract
{
    public interface IMediaManager
    {
        bool DoesTweetContainMedia { get; set; }

        void Process(List<Media> Media);
    }
}