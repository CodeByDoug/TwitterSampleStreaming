using System.Collections.Generic;

namespace Dcm.Twitter.Repository.Contract
{
    public interface IEmojiRepository
    {
        void AddProcessedItem(Models.Emoji emoji);

        void Clear();

        IEnumerable<Models.Emoji> GetAllProcessedItems();
    }
}
