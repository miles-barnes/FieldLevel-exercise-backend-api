using Fieldlevel.Models;
using System.Collections.Generic;
using System.Linq;

namespace Fieldlevel.Services
{
    public class PostService : IPostService
    {
        private readonly IPostProvider _postProvider;

        public PostService(IPostProvider postProvider)
        {
            _postProvider = postProvider;
        }

        public IEnumerable<Post> GetMostRecentPosts()
        {
            return _postProvider.GetPosts()
                .GroupBy(x => x.UserId)
                .Select(g => g.OrderByDescending(x => x.Id).First())
                .ToArray();
        }
    }
}