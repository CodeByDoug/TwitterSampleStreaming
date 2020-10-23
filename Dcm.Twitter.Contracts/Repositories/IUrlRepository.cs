using Dcm.Twitter.Models;
using System.Collections.Generic;

namespace Dcm.Twitter.Repository.Contract
{
    public interface IUrlRepository
    {
        void AddProcessedItem(Url url);

        void Clear();

        IEnumerable<Url> GetAllProcessedItems();
    }
}
