using SebnLibrary.ModelEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SebnLibrary.Repo.Interfaces
{
    public interface IEmpEvaRepo
    {
        Task<List<EmpEva>> GetEvaluationsAsync();
        Task<bool> DeleteEvaluationAsync(int id);
        Task<bool> AddEvaAsync(EmpEva empEva);

        Task<EmpEva> GetByMat(int mat);

    }
}
