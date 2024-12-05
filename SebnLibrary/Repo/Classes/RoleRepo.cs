using Microsoft.EntityFrameworkCore;
using SebnLibrary.Abstract;
using SebnLibrary.ModelEF;
using SebnLibrary.Repo.Interfaces;
using System.Linq.Expressions;

namespace SebnLibrary.Repo.Classes
{
    public class RoleRepo : IRoleRepo
    {
        private readonly SEBNDbLibDbContext _context;
        IRepository<Role> _role;
        public RoleRepo(SEBNDbLibDbContext context, IRepository<Role> role)
        {
            _context = context;
            _role = role;
        }

        public async Task<bool> AddRoleAsync(Role role)
        {
            try
            {
                await _context.Set<Role>().AddAsync(role);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Handle or log the exception as needed
                return false;
            }
        }

        public async Task<bool> DeleteRoleAsync(int IdR)
        {
            try
            {
                var roles = await RechFiltreAsync(user => user.IdR == IdR);

                if (roles == null || roles.Count == 0)
                {
                    return false;
                }

                var roleToDelete = roles.FirstOrDefault();

                if (roleToDelete != null)
                {
                    await _role.DeleteAsync(IdR);
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<List<Role>> GetAll()
        {
            try
            {
                return await _context.Set<Role>().ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<Role> GetById(int id)
        {
            try
            {
                return await _context.Set<Role>().FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }



        public async Task<bool> UpdateRoleAsync(int id, Role updatedRole)
        {
            try
            {
                var existingRole = await _context.Set<Role>().FindAsync(id);
                if (existingRole == null)
                {
                    return false;
                }

                existingRole.DescR = updatedRole.DescR;

                _context.Set<Role>().Update(existingRole);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        public async Task<List<Role>> RechFiltreAsync(Expression<Func<Role, bool>> predicate)
        {
            try
            {
                return await _context.Set<Role>().Where(predicate)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
