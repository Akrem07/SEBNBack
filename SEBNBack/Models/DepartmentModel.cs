using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SEBNBack.Models
{
    public class DepartmentModel
    {
        public int IdDep { get; set; }
        public string? NameDep { get; set; }
        public string? Post { get; set; }


    }
}
