using Microsoft.EntityFrameworkCore;
using SebnLibrary.Abstract;
using SebnLibrary.ModelEF;
using SebnLibrary.Repo.Interfaces;
using System.Linq.Expressions;

namespace SebnLibrary.Repo.Classes
{
    public class DepartmentRepo : IDepartmentRepo
    {
        private readonly SEBNDbLibDbContext _context;
        IRepository<Department> _department;

        public DepartmentRepo(SEBNDbLibDbContext context, IRepository<Department> department)
        {
            _context = context;
            _department = department;
        }
        public async Task<bool> AddDepAsync(Department department)
        {
            try
            {
                await _department.CreateAsync(department);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> UpdateDepAsync(Department department)
        {
            try
            {
                await _department.UpdateAsync(department);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<bool> EditDepAsync(int IdDep, Department UpdatedDep)
        {
            try
            {
                var deps = await RechFiltreAsync(department => department.IdDep == IdDep);

                if (deps == null || deps.Count == 0)
                {
                    return false;
                }

                var depToEdit = deps.FirstOrDefault();

                if (depToEdit != null)
                {
                    depToEdit.NameDep = UpdatedDep.NameDep;
                    depToEdit.Post = UpdatedDep.Post;


                    // Add any other properties that need to be updated

                    await _department.UpdateAsync(depToEdit);
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



        public async Task<bool> DeleteDepAsync(int IdDep)
        {
            try
            {
                var deps = await RechFiltreAsync(department => department.IdDep == IdDep);

                if (deps == null || deps.Count == 0)
                {
                    return false;
                }

                var depToDelete = deps.FirstOrDefault();

                if (depToDelete != null)
                {
                    await _department.DeleteAsync(IdDep);
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



        public async Task<ICollection<Department>> GetAll()
        {
            var listDep = await _context.Departments
                .ToListAsync();
            return listDep;
        }

        public async Task<List<Department>> GetDepByName(string NameDep)
        {
            return await _department.GetByNameAsync(NameDep);
        }

        public async Task<List<Department>> RechFiltreAsync(Expression<Func<Department, bool>> predicate)
        {
            try
            {
                return await _context.Set<Department>().Where(predicate)
                   .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Department> GetById(int id)
        {
            try
            {
                return await _context.Set<Department>().FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
