using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SEBNBack.Models
{
    public class IntegrationPlanModel
    {
        public int IdIp { get; set; }
        public string? NameIp { get; set; }

        public string? RowData { get; set; }
        public byte[]? FileData { get; set; }

    }
}
