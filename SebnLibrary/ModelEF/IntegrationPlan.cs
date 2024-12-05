using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SebnLibrary.ModelEF;

[Table("IntegrationPlan")]
public partial class IntegrationPlan
{
    [Key]
    [Column("IdIP")]
    public int IdIp { get; set; }

    [Column("NameIP")]
    [StringLength(50)]
    [Unicode(false)]
    public string? NameIp { get; set; }

    public string? RowData { get; set; }

    public byte[]? FileData { get; set; }

    [InverseProperty("IdIpNavigation")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
