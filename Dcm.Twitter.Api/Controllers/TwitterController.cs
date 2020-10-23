using System.Threading.Tasks;
using Dcm.Twitter.Business.Contract;
using Dcm.Twitter.Contracts;
using Dcm.Twitter.Models;
using Microsoft.AspNetCore.Mvc;

namespace TwitterApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TwitterController : ControllerBase
    {
        private readonly IStreamManager _streamManager;

        public TwitterController(IStreamManager streamManager)
        {
            _streamManager = streamManager;
        }

        [HttpGet]
        [Route("StartTweetStreaming")]
        public async Task<ActionResult<bool>> StartTweetStreamingAsync()
        {
            _streamManager.StartTweetStreamingAsync();
            return Ok(true);
        }

        [HttpGet]
        [Route("StartTweetStreaming/{minutes}")]
        public async Task<ActionResult<IStreamDetails>> StartTweetStreamingAsync(int minutes)
        {
            var response = _streamManager.StartTweetStreamingAsync(minutes);
            return Ok(response);
        }

        [HttpGet]
        [Route("StopTweetStreaming")]
        public async Task<ActionResult<bool>> StopTweetStreamingAsync()
        {
            return Ok(_streamManager.StopTweetStreamingAsync());
        }

        [HttpGet]
        [Route("Stats")]
        public ActionResult<IStreamDetails> GetStreamStats()
        {
            return Ok(_streamManager.GetStreamStats());
        }
    }
}
