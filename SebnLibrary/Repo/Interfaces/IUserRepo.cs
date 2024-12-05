using SebnLibrary.ModelEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SebnLibrary.Repo.Interfaces
{
    public interface IUserRepo
    {

        Task<bool> AddUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        //Task<bool> EditUserAsync(int Mat, User UpdatedUser);
        Task<bool> EditUserAsync(User User);
        Task<bool> DeleteUserAsync(int Mat);

        Task<List<User>> RechFiltreAsync(Expression<Func<User, bool>> predicate);
        Task<ICollection<User>> GetAll();
        Task<User> GetUserByMat(int Mat);
        Task<List<User>> GetUserByName(string FirstName);
        Task<List<User>> GetUserByLastName(string LastName);

        Task<User> GetUserByResp(int MatResp);
        Task<User> GetFicheByMat(int Mat);

    }
}
