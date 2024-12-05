using SebnLibrary.ModelEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SebnLibrary.Repo.Interfaces
{
    public interface IRoleRepo
    {
        Task<List<Role>> GetAll();
        Task<Role> GetById(int id);
        Task<bool> AddRoleAsync(Role role);
        Task<bool> DeleteRoleAsync(int IdR);
        Task<bool> UpdateRoleAsync(int id, Role updatedRole);
        Task<List<Role>> RechFiltreAsync(Expression<Func<Role, bool>> predicate);




    }
}
