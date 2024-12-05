using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SebnLibrary.ModelEF;

[Table("RespEva")]
public partial class RespEva
{
    [Key]
    [Column("IdREva")]
    public int IdReva { get; set; }

    public string? Forces { get; set; }

    public string? Aspects { get; set; }

    [Column("adaptation")]
    [StringLength(50)]
    public string? Adaptation { get; set; }

    [Column("autonomy")]
    [StringLength(50)]
    public string? Autonomy { get; set; }

    [Column("quality")]
    [StringLength(50)]
    public string? Quality { get; set; }

    [Column("goals")]
    [StringLength(50)]
    public string? Goals { get; set; }

    [Column("others")]
    [StringLength(50)]
    public string? Others { get; set; }

    [Column("specifiqueCommentaire1")]
    public string? SpecifiqueCommentaire1 { get; set; }

    [Column("evaluation1")]
    [StringLength(50)]
    public string? Evaluation1 { get; set; }

    public string? ProbationReussie { get; set; }

    [Column("evaluation")]
    [StringLength(50)]
    public string? Evaluation { get; set; }

    public string? ProbationNonReussie { get; set; }

    [Column("mat")]
    public int Mat { get; set; }

    [ForeignKey("Mat")]
    [InverseProperty("RespEvas")]
    public virtual User MatNavigation { get; set; } = null!;
}
