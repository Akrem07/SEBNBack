using SebnLibrary.ModelEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SebnLibrary.Repo.Interfaces
{
    public interface IIntegrationPlanRepo
    {
        Task<List<IntegrationPlan>> GetAll();
        Task<IntegrationPlan> GetById(int id);
        Task<bool> AddIPAsync(IntegrationPlan integrationPlan);
        Task<bool> DeleteIPAsync(int id);
        Task<bool> UpdateIPAsync(int id, IntegrationPlan updatedIP);

        Task<byte[]?> GetExcelFileDataByUserMat(int mat);

    }
}
