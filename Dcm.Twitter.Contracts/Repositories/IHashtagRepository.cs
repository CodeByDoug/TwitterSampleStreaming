using Dcm.Twitter.Models;
using System.Collections.Generic;

namespace Dcm.Twitter.Repository.Contract
{
    public interface IHashtagRepository
    {
        void AddProcessedItem(Hashtag hashtag);
        
        void Clear();

        IEnumerable<Hashtag> GetAllProcessedItems();
    }
}
