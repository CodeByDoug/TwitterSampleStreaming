using Dcm.Twitter.Business.Contract;
using System.Collections.Generic;
using Dcm.Twitter.Entity;

namespace Dcm.Business.Manager
{
    public class MediaManager : IMediaManager
    {
        public bool DoesTweetContainMedia { get; set; }

        public void Process(List<Media> Media)
        {
            DoesTweetContainMedia = Media?.Count > 0;
        }
    }
}