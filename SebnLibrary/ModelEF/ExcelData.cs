using System.ComponentModel.DataAnnotations;

namespace SebnLibrary.ModelEF
{
    public class ExcelData
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        public string? FileName { get; set; }

        public string? RowData { get; set; }
    }
}
