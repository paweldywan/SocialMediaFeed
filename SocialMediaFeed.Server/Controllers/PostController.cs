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
        public async Task<IActionResult> Add(PostToAdd post)
        {
            await postService.Add(post);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(PostToUpdate post)
        {
            await postService.Update(post);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await postService.Delete(id);

            return Ok();
        }

        [HttpPost("{id}/like")]
        public async Task<IActionResult> Like(int id)
        {
            await postService.ToggleLike(id);

            return Ok();
        }
    }
}
