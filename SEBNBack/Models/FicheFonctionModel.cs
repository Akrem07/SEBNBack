using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SEBNBack.Models
{
    public class FicheFonctionModel
    {
        public int IdFf { get; set; }
        public string? NameFf { get; set; }

        public string? ContentType { get; set; }

        public byte[]? FileData { get; set; }

        public int? Mresp { get; set; }
    }
}
