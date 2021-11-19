using Fieldlevel.Models;
using Fieldlevel.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fieldlevel.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecentPostController : ControllerBase
    {
        private readonly IPostService _postService;

        public RecentPostController(IPostService postService)
        {
            _postService = postService;
        }

        /// <summary>
        /// Gets the most recent post for each user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Post> Get() => _postService.GetMostRecentPosts();
    }
}
