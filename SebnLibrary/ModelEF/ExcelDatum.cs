using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SebnLibrary.ModelEF;

public partial class ExcelDatum
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public string? FileName { get; set; }

    public string? RowData { get; set; }
}
