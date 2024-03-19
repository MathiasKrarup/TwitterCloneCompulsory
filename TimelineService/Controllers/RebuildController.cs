using Microsoft.AspNetCore.Mvc;
using SharedMessage;
using TimelineApplication.Interfaces;

namespace TimelineService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RebuildController : ControllerBase
    {
        private ITimelineService _timelineService;
        private readonly MessageClient _messageClient;


        public RebuildController(ITimelineService timelineService, MessageClient messageClient)
        {
            _timelineService = timelineService;
            _messageClient = messageClient;
        }

        [HttpGet]
        public IActionResult Rebuild()
        {
            _timelineService.Rebuild();
            _messageClient.Send<TimelineMessage>(new TimelineMessage { Message = "Timeline Database Rebuilt!" }, "timeline-message");

            return Ok();
        }
    }
}
