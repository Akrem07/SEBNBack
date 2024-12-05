using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SebnLibrary.ModelEF;

[Table("EmpEva")]
public partial class EmpEva
{
    [Key]
    public int IdEva { get; set; }

    [StringLength(50)]
    public string AccueilQualite { get; set; } = null!;

    [StringLength(50)]
    public string LivretInfos { get; set; } = null!;

    [StringLength(50)]
    public string ServiceAccueil { get; set; } = null!;

    public string? AccueilCommentaires { get; set; }

    [StringLength(50)]
    public string GeneriqueFormationQualite { get; set; } = null!;

    [StringLength(50)]
    public string GeneriqueEncadrement { get; set; } = null!;

    [StringLength(50)]
    public string GeneriqueConditions { get; set; } = null!;

    public string? GeneriqueCommentaires { get; set; }

    [StringLength(50)]
    public string SpecifiqueFormationQualite { get; set; } = null!;

    [StringLength(50)]
    public string SpecifiqueEncadrement { get; set; } = null!;

    [StringLength(50)]
    public string SpecifiqueConditions { get; set; } = null!;

    [StringLength(50)]
    public string ObjectifsClairs { get; set; } = null!;

    [StringLength(50)]
    public string PerimetrePoste { get; set; } = null!;

    public string? SpecifiqueCommentaires { get; set; }

    public int? Mat { get; set; }

    [ForeignKey("Mat")]
    [InverseProperty("EmpEvas")]
    public virtual User? MatNavigation { get; set; }
}
