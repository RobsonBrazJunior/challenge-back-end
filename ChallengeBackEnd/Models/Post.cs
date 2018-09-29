using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeBackEnd.Models
{
    public class Post
    {
        public long? PostID { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public int Views { get; set; }
        public int Likes { get; set; }
    }
}
