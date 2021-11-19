using Fieldlevel.Models;
using System.Collections.Generic;

namespace Fieldlevel.Services
{
    public interface IPostProvider
    {
        /// <summary>
        /// Gets all user posts
        /// </summary>
        /// <returns></returns>
        IEnumerable<Post> GetPosts();
    }
}
