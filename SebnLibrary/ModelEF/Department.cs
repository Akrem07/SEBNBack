using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SebnLibrary.ModelEF;

[Table("Department")]
public partial class Department
{
    [Key]
    public int IdDep { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? NameDep { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Post { get; set; }

    [InverseProperty("IdDepNavigation")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
