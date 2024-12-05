using System.ComponentModel.DataAnnotations;

namespace SEBNBack.Models
{
    public class ExcelDataModel
    {
       
        public int Id { get; set; }

        public string? FileName { get; set; }

        public string? RowData { get; set; }
    }
}
