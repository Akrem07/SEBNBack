using SebnLibrary.ModelEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SebnLibrary.Repo.Interfaces
{
    public interface IRespEvaRepo
    {
        Task<List<RespEva>> GetEvaluationsAsync();
        Task<bool> DeleteEvaluationAsync(int id);
        Task<bool> AddEvaAsync(RespEva empEva);

        Task<RespEva> GetByMat(int mat);
    }
}
