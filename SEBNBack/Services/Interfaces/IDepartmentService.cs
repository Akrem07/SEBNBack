using SEBNBack.Models;
using System.Linq.Expressions;

namespace SEBNBack.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<ICollection<DepartmentModel>> GetAll();
        Task<bool> AddDepAsync(DepartmentModel department);
        Task<bool> UpdateDepAsync(DepartmentModel department);
        Task<bool> DeleteDepAsync(int IdDep);
        Task<bool> EditDepAsync(DepartmentModel department);
        Task<List<DepartmentModel>> RechFiltreAsync(Expression<Func<DepartmentModel, bool>> predicate);
        Task<List<DepartmentModel>> GetDepByName(string NameDep);
        Task<DepartmentModel> GetById(int IdDep);

    }
}
