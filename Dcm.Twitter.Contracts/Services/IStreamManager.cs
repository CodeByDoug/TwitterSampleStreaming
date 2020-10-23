using Dcm.Twitter.Contracts;
using System.Threading.Tasks;

namespace Dcm.Twitter.Business.Contract
{
    public interface IStreamManager
    {
        Task StartTweetStreamingAsync();

        Task<IStreamDetails> StartTweetStreamingAsync(int minutes);

        Task<IStreamDetails> StopTweetStreamingAsync();

        IStreamDetails GetStreamStats();
    }
}