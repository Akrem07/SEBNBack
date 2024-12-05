using SEBNBack.Models;

namespace SEBNBack.Services.Interfaces
{
    public interface IRespEvaService
    {
        Task<bool> DeleteEvaluationAsync(int id);
        Task<bool> AddEvaAsync(RespEvaModel empEva);

        Task<ICollection<RespEvaModel>> GetAll();

        Task<RespEvaModel> GetBymat(int mat);
    }
}
