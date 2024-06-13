using Microsoft.AspNetCore.Mvc;
using SocialMediaFeed.BLL.Interfaces;

namespace SocialMediaFeed.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController(IPostService postService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await postService.Get();

            return Ok(result);
        }
    }
}
