using Microsoft.AspNetCore.Mvc;
using UserApplication.Interfaces;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RebuildController : ControllerBase
    {
        private IUserCrud _userCrud;

        public RebuildController(IUserCrud userCrud)
        {
            _userCrud = userCrud;
        }


        [HttpGet]
        public IActionResult Rebuild()
        {
            _userCrud.Rebuild();
            return Ok();
        }
    }
}
