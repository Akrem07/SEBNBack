using SEBNBack.Models;
using SebnLibrary.ModelEF;

namespace SEBNBack.Services.Interfaces
{
    public interface IIntegrationPlanService
    {
        Task<List<IntegrationPlanModel>> GetAll();
        Task<IntegrationPlanModel> GetById(int id);
        Task<bool> AddIPAsync(IntegrationPlanModel integrationPlan);
        Task<bool> DeleteIPAsync(int id);
        Task<bool> UpdateIPAsync(IntegrationPlanModel integrationPlan);
        //Task<bool> ProcessExcelFileAsync(Stream fileStream);


        IntegrationPlanModel ProcessExcel(string name, int id, byte[] fileData);
        Task SaveExcelDataAsync(IntegrationPlanModel integrationPlan);
        Task<byte[]?> GetExcelFileDataByUserMat(int mat);
    }
}
