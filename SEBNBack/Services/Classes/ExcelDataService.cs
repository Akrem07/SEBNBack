using OfficeOpenXml;
using SEBNBack.Services.Interfaces;
using SebnLibrary.ModelEF;
using SebnLibrary.Repo.Interfaces;

namespace SEBNBack.Services.Classes
{
    public class ExcelDataService : IExcelDataService
    {
        private readonly IExcelDataRepo _excelDataRepo;

        public ExcelDataService(IExcelDataRepo excelDataRepo)
        {
            _excelDataRepo = excelDataRepo;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public ExcelData ProcessExcel(byte[] fileData)
        {
            using (var stream = new MemoryStream(fileData))
            {
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;
                    var columnCount = worksheet.Dimension.Columns;

                    var rowData = new List<string>();
                    for (int row = 1; row <= rowCount; row++)
                    {
                        for (int col = 1; col <= columnCount; col++)
                        {
                            rowData.Add(worksheet.Cells[row, col].Text);
                        }
                    }

                    return new ExcelData
                    {
                        FileName = "UploadedExcelFile",
                        RowData = string.Join(",", rowData)
                    };
                }
            }
        }

        public async Task SaveExcelDataAsync(ExcelData excelData)
        {
            await _excelDataRepo.AddAsync(excelData);
        }
    }
}
