using SEBNBack.Models;
using System.Linq.Expressions;

namespace SEBNBack.Services.Interfaces
{
    public interface IRoleService
    {
        Task<ICollection<RoleModel>> GetAll();
        Task<RoleModel> GetById(int IdR);
        Task<bool> AddRoleAsync(RoleModel role);
        Task<bool> DeleteRoleAsync(int IdR);
        Task<bool> UpdateRoleAsync(RoleModel role);
        Task<List<RoleModel>> RechFiltreAsync(Expression<Func<RoleModel, bool>> predicate);

    }
}
