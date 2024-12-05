using SebnLibrary.ModelEF;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SEBNBack.Models
{
    public class RespEvaModel
    {

      
        public int IdReva { get; set; }

        public string? Forces { get; set; }

        public string? Aspects { get; set; }


        public string Adaptation { get; set; } = null!;


        public string Autonomy { get; set; } = null!;

      
        public string Quality { get; set; } = null!;

        public string Goals { get; set; } = null!;

        public string Others { get; set; } = null!;


        public string? SpecifiqueCommentaire1 { get; set; }


        public string Evaluation1 { get; set; } = null!;

        public string? ProbationReussie { get; set; }

        public string Evaluation { get; set; } = null!;

        public string? ProbationNonReussie { get; set; }

        public int Mat { get; set; }



    }
}
