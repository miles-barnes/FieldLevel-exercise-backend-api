using Fieldlevel.Models;
using System.Collections.Generic;

namespace Fieldlevel.Services
{
    public interface IPostService
    {
        /// <summary>
        /// Gets the most recent post for each user
        /// </summary>
        /// <returns></returns>
        IEnumerable<Post> GetMostRecentPosts();
    }
}
