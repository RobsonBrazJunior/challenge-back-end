using System.Collections.Generic;
using System.Linq;

namespace ChallengeBackEnd.Models
{
    public class PostsViewModel
    {
        public int? TotalDeLikes => Posts?.Sum(p => p.Likes);
        public int? TotalDeViews => Posts?.Sum(p => p.Views);
        public List<Post> Posts { get; set; }
    }
}
