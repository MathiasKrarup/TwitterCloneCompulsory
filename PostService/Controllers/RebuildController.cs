using Microsoft.AspNetCore.Mvc;
using PostApplication.Interfaces;
using SharedMessage;

namespace PostService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RebuildController : Controller
    {
        private IPostCrud _postCrud;
        private readonly MessageClient _messageClient;

        public RebuildController(IPostCrud postCrud, MessageClient messageClient)
        {
            _postCrud = postCrud;
            _messageClient = messageClient;
        }

        [HttpGet]
        public IActionResult Rebuild()
        {
            _postCrud.Rebuild();
            _messageClient.Send(new PostMessage { Message = "Post Database Rebuilt!" }, "post-message");
            return Ok();
        }
    }
}
