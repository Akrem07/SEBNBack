using SebnLibrary.ModelEF;
using System.Threading.Tasks;

namespace SEBNBack.Services.Interfaces
{
    public interface IExcelDataService
    {
        ExcelData ProcessExcel(byte[] fileData);
        Task SaveExcelDataAsync(ExcelData excelData);
    }
}
