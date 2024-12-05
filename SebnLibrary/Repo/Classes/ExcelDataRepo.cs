using SebnLibrary.Abstract;
using SebnLibrary.ModelEF;
using SebnLibrary.Repo.Interfaces;

namespace SebnLibrary.Repo.Classes
{
    public class ExcelDataRepo : Repository<ExcelData>, IExcelDataRepo
    {
        public ExcelDataRepo(SEBNDbLibDbContext context) : base(context)
        {
        }
    }
}
