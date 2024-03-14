using Microsoft.AspNetCore.Mvc;
using TimelineApplication.Interfaces;

namespace TimelineService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RebuildController : ControllerBase
    {
        private ITimelineService _timelineService;

        public RebuildController(ITimelineService timelineService)
        {
            _timelineService = timelineService;
        }

        [HttpGet]
        public IActionResult Rebuild()
        {
            _timelineService.Rebuild();
            return Ok();
        }
    }
}
