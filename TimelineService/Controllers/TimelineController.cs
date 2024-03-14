using Microsoft.AspNetCore.Mvc;
using SharedMessages;
using TimelineApplication.Interfaces;

namespace TimelineService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimelineController : ControllerBase
    {
        private readonly ITimelineService _timelineService;
        private readonly MessageClient _messageClient;

        public TimelineController(ITimelineService timelineService, MessageClient messageClient)
        {
            _timelineService = timelineService;
            _messageClient = messageClient;
        }

        [HttpPost]
        public bool TestTimeMsg()
        {
            _messageClient.Send<TimelineMessage>(new TimelineMessage{Message = "Timeline recived!" }, "timeline-message");
            return true;
        }

        [HttpPost]
        public async Task<IActionResult> AddPostToTimeline(int postId)
        {
            try
            {
                await _timelineService.AddPostToTimelineAsync(postId);
                return Ok(postId);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500, "An error occured while trying to add the post to the timeline");
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetTimeLine()
        {
            try
            {
                var postIds = await _timelineService.GetTimelinePostIdsAsync();
                return Ok(postIds);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occured while retrieving the timeline");
            }
            
        }

        [HttpDelete]
        public async Task<IActionResult> RemovePostFromTimeline(int postId)
        {
            try
            {
                await _timelineService.RemovePostFromTimelineAsync(postId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occured while removing the post from the timeline");
            }
        }

        
    }
}
