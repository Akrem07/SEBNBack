using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SebnLibrary.ModelEF;

[Table("User")]
[Index("Mat", Name = "UQ__User__C7977BC56132203D", IsUnique = true)]
public partial class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int? Mat { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? FirstName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? LastName { get; set; }

    [StringLength(256)]
    [Unicode(false)]
    public string? Password { get; set; }

    [Column("IdFF")]
    public int? IdFf { get; set; }

    public int IdR { get; set; }

    public int IdDep { get; set; }

    [Column("IdIP")]
    public int? IdIp { get; set; }

    public string? Token { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenExpiryTime { get; set; }

    public int? MatResp { get; set; }

    [InverseProperty("MatNavigation")]
    public virtual ICollection<EmpEva> EmpEvas { get; set; } = new List<EmpEva>();

    [InverseProperty("MrespNavigation")]
    public virtual ICollection<FicheFonction> FicheFonctions { get; set; } = new List<FicheFonction>();

    [ForeignKey("IdDep")]
    [InverseProperty("Users")]
    public virtual Department IdDepNavigation { get; set; } = null!;

    [ForeignKey("IdFf")]
    [InverseProperty("Users")]
    public virtual FicheFonction? IdFfNavigation { get; set; }

    [ForeignKey("IdIp")]
    [InverseProperty("Users")]
    public virtual IntegrationPlan? IdIpNavigation { get; set; }

    [ForeignKey("IdR")]
    [InverseProperty("Users")]
    public virtual Role IdRNavigation { get; set; } = null!;

    [InverseProperty("MatRespNavigation")]
    public virtual ICollection<User> InverseMatRespNavigation { get; set; } = new List<User>();

    [ForeignKey("MatResp")]
    [InverseProperty("InverseMatRespNavigation")]
    public virtual User? MatRespNavigation { get; set; }

    [InverseProperty("MatNavigation")]
    public virtual ICollection<RespEva> RespEvas { get; set; } = new List<RespEva>();
}
