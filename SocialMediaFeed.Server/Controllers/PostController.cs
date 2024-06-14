using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaFeed.BLL.Interfaces;
using SocialMediaFeed.BLL.Models;

namespace SocialMediaFeed.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PostController(IPostService postService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await postService.Get();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SimplePost post)
        {
            await postService.Add(post);

            return Ok();
        }
    }
}
