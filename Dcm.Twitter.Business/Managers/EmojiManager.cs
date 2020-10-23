using Dcm.Twitter.Repository.Contract;
using NeoSmart.Unicode;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Dcm.Twitter.Business.Contract;

namespace Dcm.Business.Manager
{
    public class EmojiManager : IEmojiManager
    {
        const string Emojipattern = @"(\u00a9|\u00ae|[\u2000-\u3300]|\ud83c[\ud000-\udfff]|\ud83d[\ud000-\udfff]|\ud83e[\ud000-\udfff])";
        private readonly IEmojiRepository _emojiRepository;

        public EmojiManager(IEmojiRepository emojiRepository)
        {
            _emojiRepository = emojiRepository;
        }

        public void Process(string message)
        {
            DoesTweetContainsEmojis = false;

            var regex = new Regex(Emojipattern);
          
            foreach (Match match in regex.Matches(message))
            {
                foreach (var emoji in Emoji.All)
                {
                    if (emoji.Sequence.AsString != match.Value) continue;

                    _emojiRepository.AddProcessedItem(new Dcm.Twitter.Models.Emoji { Name = emoji.Name });
                    break;
                }

                if (Emoji.IsEmoji(match.Value))
                {
                    DoesTweetContainsEmojis = true;
                }
            }
        }

        public bool DoesTweetContainsEmojis { get; set; }

        public IEnumerable<string> GetMostPopularEmojis(int count)
        {
            return _emojiRepository.GetAllProcessedItems().ToList().GroupBy(p => p.Name).OrderByDescending(p => p.Count()).Take(count).Select(p => p.Key).ToList();
        }

        public void Reset()
        {
            _emojiRepository.Clear();
        }
    }
}