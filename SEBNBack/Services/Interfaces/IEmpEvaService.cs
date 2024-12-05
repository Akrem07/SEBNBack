using SEBNBack.Models;
using SebnLibrary.ModelEF;

namespace SEBNBack.Services.Interfaces
{
    public interface IEmpEvaService
    {
        
        Task<bool> DeleteEvaluationAsync(int id);
        Task<bool> AddEvaAsync(EmpEvaModel empEva);

        Task<ICollection<EmpEvaModel>> GetAll();

        Task<EmpEvaModel> GetBymat(int mat);
    }
}
