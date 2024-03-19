using Microsoft.AspNetCore.Mvc;
using SharedMessage;
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
        public async Task<IActionResult> AddPostToTimeline(int postId)
        {
            try
            {
                await _timelineService.AddPostToTimelineAsync(postId);
                _messageClient.Send<TimelineMessage>(new TimelineMessage { Message = "Post Added to Timeline!" }, "timeline-message");
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
                _messageClient.Send<TimelineMessage>(new TimelineMessage { Message = "Got All Posts From Timeline!" }, "timeline-message");

                return Ok(postIds);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occured while retrieving the timeline");
            }
            
        }

        [HttpDelete("{postId}")]
        public async Task<IActionResult> RemovePostFromTimeline(int postId)
        {
            try
            {
                await _timelineService.RemovePostFromTimelineAsync(postId);
                _messageClient.Send<TimelineMessage>(new TimelineMessage { Message = "Post Removed From Timeline!" }, "timeline-message");

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Post not found on the timeline");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occured while removing the post from the timeline");
            }
        }

        
    }
}
