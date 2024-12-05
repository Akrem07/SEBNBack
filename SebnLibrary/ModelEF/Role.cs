using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SebnLibrary.ModelEF;

[Table("Role")]
public partial class Role
{
    [Key]
    public int IdR { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? DescR { get; set; }

    [InverseProperty("IdRNavigation")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
