using Microsoft.AspNetCore.Mvc;
using PostApplication.Interfaces;

namespace PostService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RebuildController : Controller
    {
        private IPostCrud _postCrud;
        public RebuildController(IPostCrud postCrud){
            _postCrud = postCrud;
        }

        [HttpGet]
        public IActionResult Rebuild()
        {
            _postCrud.Rebuild();
            return Ok();
        }
    }
}
