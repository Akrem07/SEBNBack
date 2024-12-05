using SebnLibrary.ModelEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SebnLibrary.Repo.Interfaces
{
        public interface IDepartmentRepo
        {
            Task<ICollection<Department>> GetAll();
            Task<List<Department>> GetDepByName(string NameDep);
            Task<bool> AddDepAsync(Department department);
            Task<Department> GetById(int id);
            Task<bool> UpdateDepAsync(Department department);
            Task<bool> DeleteDepAsync(int IdDep);
            Task<bool> EditDepAsync(int IdDep, Department UpdatedDep);
            Task<List<Department>> RechFiltreAsync(Expression<Func<Department, bool>> predicate);
        }
    }

