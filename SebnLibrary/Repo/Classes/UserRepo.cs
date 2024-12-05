using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SebnLibrary.Abstract;
using SebnLibrary.ModelEF;
using SebnLibrary.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SebnLibrary.Repo.Classes
{
    public class UserRepo : IUserRepo
    {
        private readonly SEBNDbLibDbContext _context;
 
        IRepository<User> _user;

        public UserRepo(SEBNDbLibDbContext context, IRepository<User> user)
        {
            _context = context;
            _user = user;
        }

        public async Task<bool> AddUserAsync(User user)
        {
            try
            {
                await _user.CreateAsync(user);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> DeleteUserAsync(int Mat)
        {

            try
            {
                var users = await RechFiltreAsync(user => user.Mat == Mat);

                if (users == null || users.Count == 0)
                {
                    return false;
                }

                var userToDelete = users.FirstOrDefault();

                if (userToDelete != null)
                {
                    await _user.DeleteAsync(Mat);
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

        //public async Task<bool> EditUserAsync(int Mat, User UpdatedUser)
        //{
        //    try
        //    {
        //        var users = await RechFiltreAsync(user => user.Mat == Mat);

        //        if (users == null || users.Count == 0)
        //        {
        //            return false;
        //        }

        //        var userToEdit = users.FirstOrDefault();

        //        if (userToEdit != null)
        //        {
        //            userToEdit.FirstName = UpdatedUser.FirstName;
        //            userToEdit.LastName = UpdatedUser.LastName;
        //            userToEdit.Password = UpdatedUser.Password;
        //            userToEdit.IdDep= UpdatedUser.IdDep;
        //            userToEdit.IdR = UpdatedUser.IdR;
        //            userToEdit.IdFf = UpdatedUser.IdFf;
        //            userToEdit.IdIp = UpdatedUser.IdIp;
        //            userToEdit.IdDepNavigation = UpdatedUser.IdDepNavigation;
        //            userToEdit.IdFfNavigation = UpdatedUser.IdFfNavigation;
        //            userToEdit.IdIpNavigation = UpdatedUser.IdIpNavigation;
        //            userToEdit.IdRNavigation = UpdatedUser.IdRNavigation;
        //            // Add any other properties that need to be updated

        //            await _user.UpdateAsync(userToEdit);
        //            await _context.SaveChangesAsync();
        //            return true;
        //        }

        //        return false;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}


        public async Task<bool> EditUserAsync(User User)
        {
            try
            {
                await _user.UpdateAsync(User);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<User>> RechFiltreAsync(Expression<Func<User, bool>> predicate)
        {
            try
            {
                return await _context.Set<User>().Where(predicate)
                   .Include(emetteurJoin => emetteurJoin.IdDepNavigation)
                   .Include(emetteurJoin => emetteurJoin.IdFfNavigation)
                   .Include(emetteurJoin => emetteurJoin.IdIpNavigation)
                   .Include(emetteurJoin => emetteurJoin.IdRNavigation)
                   .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ICollection<User>> GetAll()
        {
            var listUser = await _context.Users
                .Include(u => u.IdDepNavigation)
                .Include(u=>u.IdIpNavigation)
                .Include (u=>u.IdRNavigation)
                .Include (u=>u.IdFfNavigation)
                .ToListAsync();

            return listUser;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            try
            {
                await _user.UpdateAsync(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> GetUserByMat(int Mat)
        {
            var listUser = await _user.GetByIdAsync(Mat);
            return listUser;
        }

        public async Task<User> GetUserByResp(int MatResp)
        {
            var listUser = await _user.GetByIdAsync(MatResp);
            return listUser;
        }

        public async Task<List<User>> GetUserByName(string FirstName)
        {
            return await _user.GetByNameAsync(FirstName);
        }
        public async Task<List<User>> GetUserByLastName(string LastName)
        {
            return await _user.GetByLastNameAsync(LastName);
        }


        public async Task<User> GetFicheByMat(int Mat)
        {
            var listUser = await _user.GetByIdAsync(Mat);
            return listUser;
        }


    }


}
