using System.ComponentModel.DataAnnotations.Schema;

namespace ChallengeBackEnd.Models
{
    public class Post
    {
        public int PostID { get; set; }
        public string Titulo { get; set; }
        public string Resumo { get; set; }
        public string Conteudo { get; set; }
        public int Views { get; set; }
        public int Likes { get; set; }

        [NotMapped]
        public double PercentualViewsSobreTotal { get; set; }

        [NotMapped]
        public double PercentualLikesSobreTotal { get; set; }
    }
}