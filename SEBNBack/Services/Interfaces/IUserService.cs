using SEBNBack.Models;
using System.Linq.Expressions;

namespace SEBNBack.Services.Interfaces
{
    public interface IUserService
    {
        Task<ICollection<UserModel>> GetAll();
        Task<bool> AddUserAsync(AddUserModel user);
        Task<bool> UpdateUserAsync(UserModel user);
        Task<bool> EditUserAsync(AddUserModel user);
        Task<bool> DeleteUserAsync(int Mat);
        
        Task<List<UserModel>> RechFiltreAsync(Expression<Func<UserModel, bool>> predicate);
        
        Task<bool> AttributeFiche(int Mat, int IdFf);
        Task<UserModel> GetUserByMat(int mat);
        Task<List<UserModel>> GetUserByName(string firstName);
        Task<List<UserModel>> GetUserByLastName(string lastName);
        Task<bool> AttributeIntegrationPlan(int Mat, int IdIp);


        Task<String> LoginUser(int Mat, String Password);

        Task<bool> AttributeResp(int Mat, int MatResp);

        Task<UserModel> GetUserByResp(int matResp);

        Task<UserFfModel> GetFicheByMat(int mat);
    }
}
