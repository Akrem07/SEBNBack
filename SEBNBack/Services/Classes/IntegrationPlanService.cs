using OfficeOpenXml;
using SEBNBack.Models;
using SEBNBack.Services.Interfaces;
using SebnLibrary.ModelEF;
using SebnLibrary.Repo.Classes;
using SebnLibrary.Repo.Interfaces;

namespace SEBNBack.Services.Classes
{
    public class IntegrationPlanService : IIntegrationPlanService
    {
        private readonly IIntegrationPlanRepo _integrationPlanRepository;

        public IntegrationPlanService(IIntegrationPlanRepo integrationPlanRepository)
        {
            _integrationPlanRepository = integrationPlanRepository;
        }




        public async Task<bool> AddIPAsync(IntegrationPlanModel integrationPlan)
        {
            try
            {
                var integrationPlanEntity = new IntegrationPlan
                {
                    IdIp = integrationPlan.IdIp,
                    NameIp = integrationPlan.NameIp
                };

                return await _integrationPlanRepository.AddIPAsync(integrationPlanEntity);
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                return false;
            }
        }

        public async Task<bool> DeleteIPAsync(int id)
        {
            try
            {
                return await _integrationPlanRepository.DeleteIPAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the integration plan", ex);
            }
        }

        //public async Task<bool> UpdateIPAsync(int id, IntegrationPlanModel updatedIP)
        //{
        //    try
        //    {
        //        var updatedIntegrationPlan = new IntegrationPlan
        //        {
        //            IdIp = updatedIP.IdIp,
        //            NameIp = updatedIP.NameIp
        //        };

        //        return await _integrationPlanRepository.UpdateIPAsync(id, updatedIntegrationPlan);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("An error occurred while updating the integration plan", ex);
        //    }
        //}

        public async Task<bool> UpdateIPAsync(IntegrationPlanModel integrationPlan)
        {
            try
            {
                IntegrationPlan updatedIP = new IntegrationPlan
                {

                    NameIp = integrationPlan.NameIp,

                };

                // Call the UserRepo method to update the user
                return await _integrationPlanRepository.UpdateIPAsync(integrationPlan.IdIp, updatedIP);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while editing the user", ex);
            }
        }

        public async Task<List<IntegrationPlanModel>> GetAll()
        {
            try
            {
                var integrationPlans = await _integrationPlanRepository.GetAll();
                return integrationPlans.Select(ip => new IntegrationPlanModel
                {
                    IdIp = ip.IdIp,
                    NameIp = ip.NameIp
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all integration plans", ex);
            }
        }

        public async Task<IntegrationPlanModel> GetById(int id)
        {
            try
            {
                var integrationPlan = await _integrationPlanRepository.GetById(id);
                if (integrationPlan == null)
                {
                    return null;
                }

                return new IntegrationPlanModel
                {
                    IdIp = integrationPlan.IdIp,
                    NameIp = integrationPlan.NameIp
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the integration plan with ID {id}", ex);
            }
        }









        public IntegrationPlanModel ProcessExcel(string name, int id, byte[] fileData)
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var stream = new MemoryStream(fileData))
                {
                    using (var package = new ExcelPackage(stream))
                    {
                        if (package.Workbook == null)
                        {
                            throw new InvalidDataException("The Excel package does not contain a valid workbook.");
                        }

                        var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                        if (worksheet == null)
                        {
                            throw new InvalidDataException("The workbook does not contain any worksheets.");
                        }

                        var rowCount = worksheet.Dimension?.Rows;
                        var columnCount = worksheet.Dimension?.Columns;

                        if (rowCount == null || columnCount == null)
                        {
                            throw new InvalidDataException("The worksheet does not contain any data.");
                        }

                        var rowData = new List<string>();
                        for (int row = 1; row <= rowCount.Value; row++)
                        {
                            for (int col = 1; col <= columnCount.Value; col++)
                            {
                                rowData.Add(worksheet.Cells[row, col].Text);
                            }
                        }

                        Console.WriteLine($"Row data length: {rowData.Count}");
                        Console.WriteLine($"File data length: {fileData.Length}");

                        return new IntegrationPlanModel
                        {
                            IdIp = id,
                            NameIp = name,
                            RowData = string.Join(",", rowData),
                            FileData = fileData
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing Excel: {ex.Message}");
                throw new Exception("An error occurred while processing the Excel file", ex);
            }
        }

        public async Task SaveExcelDataAsync(IntegrationPlanModel integrationPlan)
        {
            var integrationPlanEntity = new IntegrationPlan
            {
                IdIp = integrationPlan.IdIp,
                NameIp = integrationPlan.NameIp,
                RowData = integrationPlan.RowData,
                FileData = integrationPlan.FileData
            };

            try
            {
                await _integrationPlanRepository.AddIPAsync(integrationPlanEntity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data: {ex.Message}");
                throw;
            }
        }


        public async Task<byte[]?> GetExcelFileDataByUserMat(int mat)
        {
            try
            {
                return await _integrationPlanRepository.GetExcelFileDataByUserMat(mat);
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"Error in GetExcelFileDataByUserMat in Service: {ex.Message}");
                throw;
            }
        }




    }
}