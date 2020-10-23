using Dcm.Twitter.Repository.Contract;
using Dcm.Twitter.Models;
using System.Collections.Generic;

namespace Dcm.Twitter.Repository
{
    public class EmojiRepository : IEmojiRepository
    {
        private readonly List<Emoji> _emojis = new List<Emoji>();

        public void AddProcessedItem(Emoji emoji)
        {
            _emojis.Add(emoji);
        }

        public void Clear()
        {
            _emojis.Clear();
        }

        public IEnumerable<Emoji> GetAllProcessedItems()
        {
            return _emojis;
        }
    }
}