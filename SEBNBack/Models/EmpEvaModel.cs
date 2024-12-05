using System.ComponentModel.DataAnnotations;

namespace SEBNBack.Models
{
    public class EmpEvaModel
    {
        public int IdEva { get; set; } 

        public string AccueilQualite { get; set; } = null!;

        public string LivretInfos { get; set; } = null!;

        public string ServiceAccueil { get; set; } = null!;

        public string? AccueilCommentaires { get; set; }

        public string GeneriqueFormationQualite { get; set; } = null!;

        public string GeneriqueEncadrement { get; set; } = null!;

        public string GeneriqueConditions { get; set; } = null!;

        public string? GeneriqueCommentaires { get; set; }

        public string SpecifiqueFormationQualite { get; set; } = null!;

        public string SpecifiqueEncadrement { get; set; } = null!;

        public string SpecifiqueConditions { get; set; } = null!;

        public string ObjectifsClairs { get; set; } = null!;

        public string PerimetrePoste { get; set; } = null!;

        public string? SpecifiqueCommentaires { get; set; }

        public int? Mat { get; set; } // Matricule of the user who submitted the evaluation
    }
}

 