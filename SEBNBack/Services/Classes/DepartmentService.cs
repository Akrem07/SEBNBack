using Microsoft.Extensions.Hosting;
using SEBNBack.Models;
using SEBNBack.Services.Interfaces;
using SebnLibrary.ModelEF;
using SebnLibrary.Repo.Interfaces;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SEBNBack.Services.Classes
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepo _departmentRepository;

        public DepartmentService(IDepartmentRepo departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<bool> AddDepAsync(DepartmentModel department)
        {
            try
            {
                bool res = false;

                Department Depadd = new Department();
                Depadd.IdDep = department.IdDep;
                Depadd.NameDep = department.NameDep;
                Depadd.Post=department.Post;

                res = await _departmentRepository.AddDepAsync(Depadd);

                if (res)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteDepAsync(int IdDep)
        {
            try
            {
                return await _departmentRepository.DeleteDepAsync(IdDep);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the Department", ex);
            }
        }

        public async Task<bool> EditDepAsync(DepartmentModel department)
        {
            try
            {
                Department updatedDep = new Department
                {
                    IdDep = department.IdDep,
                    NameDep = department.NameDep,
                    Post = department.Post
                    
                };

                return await _departmentRepository.EditDepAsync(department.IdDep, updatedDep);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while editing the department", ex);
            }
        }

        public async Task<ICollection<DepartmentModel>> GetAll()
        {
            try
            {
                var deps = await _departmentRepository.GetAll();
                var depModels = deps.Select(department => new DepartmentModel
                {
                    IdDep = department.IdDep,
                    NameDep = department.NameDep,
                    Post = department.Post
                   
                }).ToList();

                return depModels;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all departments", ex);
            }
        }

        public async Task<List<DepartmentModel>> GetDepByName(string NameDep)
        {
            try
            {
                var deps = await _departmentRepository.GetDepByName(NameDep);
                return deps.Select(department => new DepartmentModel
                {
                    IdDep = department.IdDep,
                    NameDep = department.NameDep,
                    Post = department.Post 

                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving departments by name {NameDep}", ex);
            }
        }

        public async Task<List<DepartmentModel>> RechFiltreAsync(Expression<Func<DepartmentModel, bool>> predicate)
        {
            try
            {
                var deps = await _departmentRepository.RechFiltreAsync(department => true);

                var depModels = deps.Select(department => new DepartmentModel
                {
                    IdDep = department.IdDep,
                    NameDep = department.NameDep,
                    Post = department.Post,
                }).ToList();

                return depModels.AsQueryable().Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while filtering departments", ex);
            }
        }

        public async Task<bool> UpdateDepAsync(DepartmentModel department)
        {
            try
            {
                Department depToUpdate = new Department
                {
                    NameDep = department.NameDep,
                    Post = department.Post
                };

                return await _departmentRepository.UpdateDepAsync(depToUpdate);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the department", ex);
            }
        }




        public async Task<DepartmentModel> GetById(int IdDep)
        {
            try
            {
                var dep = await _departmentRepository.GetById(IdDep);
                if (dep == null)
                {
                    return null;
                }

                return new DepartmentModel
                {
                    IdDep = dep.IdDep,
                    NameDep = dep.NameDep,
                    Post= dep.Post,
                };
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the role by ID", ex);
            }
        }

    }
}
