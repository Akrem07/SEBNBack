using SEBNBack.Models;
using SEBNBack.Services.Interfaces;
using SebnLibrary.ModelEF;
using SebnLibrary.Repo.Interfaces;
using System.Linq.Expressions;

namespace SEBNBack.Services.Classes
{
    public class RoleService :IRoleService 
    {
        private readonly IRoleRepo _roleRepository;

        public RoleService(IRoleRepo roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<bool> AddRoleAsync(RoleModel role)
        {
            try
            {
                bool res = false;

                Role roleadd = new Role();
                roleadd.IdR = role.IdR;
                roleadd.DescR = role.DescR;
                res = await _roleRepository.AddRoleAsync(roleadd);

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

        public async Task<bool> DeleteRoleAsync(int IdR)
        {
            try
            {
                return await _roleRepository.DeleteRoleAsync(IdR);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the user", ex);
            }
        }


        public async Task<List<RoleModel>> RechFiltreAsync(Expression<Func<RoleModel, bool>> predicate)
        {
            try
            {
                var roles = await _roleRepository.RechFiltreAsync(role => true);

                var roleModels = roles.Select(role => new RoleModel
                {
                    IdR = role.IdR,
                    DescR = role.DescR,
                    
                }).ToList();

                return roleModels.AsQueryable().Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while filtering roles", ex);
            }
        }





        public async Task<ICollection<RoleModel>> GetAll()
        {
            try
            {
                var roles = await _roleRepository.GetAll();
                var roleModels = roles.Select(role => new RoleModel
                {
                    IdR = role.IdR,
                    DescR = role.DescR
                }).ToList();

                return roleModels;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all users", ex);
            }
        }







        public async Task<RoleModel> GetById(int IdR)
        {
            try
            {
                var role = await _roleRepository.GetById(IdR);
                if (role == null)
                {
                    return null;
                }

                return new RoleModel
                {
                    IdR = role.IdR,
                    DescR = role.DescR
                };
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the role by ID", ex);
            }
        }


        public async Task<bool> UpdateRoleAsync(RoleModel role)
        {
            try
            {
                // Create a new User entity object
                Role updatedUser = new Role
                {
                    IdR = role.IdR,
                    DescR = role.DescR,

                };

                // Call the UserRepo method to update the user
                return await _roleRepository.UpdateRoleAsync(role.IdR, updatedUser);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while editing the user", ex);
            }
        }
    }
}
