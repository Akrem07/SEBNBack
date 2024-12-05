using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SebnLibrary.ModelEF;

[Table("FicheFonction")]
public partial class FicheFonction
{
    [Key]
    [Column("IdFF")]
    public int IdFf { get; set; }

    [Column("NameFF")]
    [StringLength(255)]
    public string? NameFf { get; set; }

    [StringLength(255)]
    public string? ContentType { get; set; }

    public byte[]? FileData { get; set; }

    public int? Mresp { get; set; }

    [ForeignKey("Mresp")]
    [InverseProperty("FicheFonctions")]
    public virtual User? MrespNavigation { get; set; }

    [InverseProperty("IdFfNavigation")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
