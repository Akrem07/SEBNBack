using Microsoft.AspNetCore.Identity;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using SEBNBack.Models;
using SEBNBack.Services.Interfaces;
using SebnLibrary.ModelEF;
using SebnLibrary.Repo.Interfaces;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;

namespace SEBNBack.Services.Classes
{

    public class UserService : IUserService
    {

        private readonly IUserRepo _userRepository;
        private readonly IFicheFonctionRepo _FfRepo;
        private readonly IIntegrationPlanRepo _integrationPlanRepo;

        public UserService(IUserRepo userRepository, IFicheFonctionRepo FfRepo, IIntegrationPlanRepo integrationPlanRepo)
        {
            _userRepository = userRepository;
            _FfRepo = FfRepo;
            _integrationPlanRepo = integrationPlanRepo;

        }



        //public async Task<bool> AddUserAsync(AddUserModel user)
        //{
        //    try
        //    {
        //        bool res = false;

        //        // Hash the password
        //        user.Password = HashPassword(user.Password);

        //        User useradd = new User
        //        {
        //            Mat = user.Mat,
        //            Id = user.Id,
        //            FirstName = user.FirstName,
        //            LastName = user.LastName,
        //            Password = user.Password,
        //            IdR = user.IdR,
        //            IdDep = user.IdDep
        //        };

        //        res = await _userRepository.AddUserAsync(useradd);

        //        return res;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}



        public async Task<bool> AddUserAsync(AddUserModel user)
        {
            var passwordHasher = new PasswordHasher<User>();
            bool res = false;
            try
            {


                User Newuser = new User();
                Newuser.Id = user.Id;
                Newuser.Mat = user.Mat;
                Newuser.FirstName = user.FirstName;
                Newuser.LastName = user.LastName;
                Newuser.Password = passwordHasher.HashPassword(null, user.Password);
                Newuser.IdR = user.IdR;
                Newuser.IdDep = user.IdDep;
  

                res = await _userRepository.AddUserAsync(Newuser);


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
                throw ex;
            }

        }


        public async Task<bool> EditUserAsync(AddUserModel User)
        {
            try
            {
                var passwordHasher = new PasswordHasher<User>();
                var Updateduser = await _userRepository.GetUserByMat((int)User.Mat);

                if (Updateduser != null)
                {
                    // Update the properties of the user entity
                    Updateduser.FirstName = User.FirstName;
                    Updateduser.LastName = User.LastName;
                    Updateduser.Password = passwordHasher.HashPassword(null, User.Password);
                    Updateduser.IdDep = User.IdDep;
                    Updateduser.IdR = User.IdR;
                    //Updateduser.IdFf = User.IdFf;
                    //Updateduser.IdIp = User.IdIp;
                    //Updateduser.MatResp = User.MatResp;
    
                    //Updateduser.Password = User.Password;

                    // Use the UpdateEntity method from the abstract repository
                    return await _userRepository.EditUserAsync(Updateduser);
                }
                else
                {
                    return false; // User not found
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //private string HashPassword(string password)
        //{
        //    using (var sha256 = SHA256.Create())
        //    {
        //        // Convert the password string to a byte array
        //        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

        //        // Convert the byte array to a string
        //        StringBuilder builder = new StringBuilder();
        //        for (int i = 0; i < bytes.Length; i++)
        //        {
        //            builder.Append(bytes[i].ToString("x2"));
        //        }

        //        return builder.ToString();
        //    }
        //}




        //public async Task<bool> AddUserAsync(AddUserModel user)
        //{
        //    try
        //    {
        //        bool res = false;

        //            User useradd = new User();
        //            useradd.Mat = user.Mat;
        //            useradd.Id = user.Id;
        //            useradd.FirstName = user.FirstName;
        //            useradd.LastName = user.LastName;
        //            useradd.Password = user.Password;
        //            useradd.IdR=user.IdR;
        //            useradd.IdDep = user.IdDep;                    

        //            res = await _userRepository.AddUserAsync(useradd);

        //        if (res)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}



        //public async Task<bool> EditUserAsync(UserModel user)
        //{
        //    try
        //    {
        //        User updatedUser = new User
        //        {
        //            Mat = user.Mat,
        //            Id = user.Id,
        //            FirstName = user.FirstName,
        //            LastName = user.LastName,
        //            Password = user.Password,
        //            IdR = user.IdR,
        //            IdDep = user.IdDep,
        //            IdFf = user.IdFf,
        //            IdIp = user.IdIp,

        //        };

        //        return await _userRepository.EditUserAsync(user.Mat, updatedUser);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("An error occurred while editing the user", ex);
        //    }
        //}


        public async Task<bool> DeleteUserAsync(int Mat)
        {
            try
            {
                return await _userRepository.DeleteUserAsync(Mat);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the user", ex);
            }
        }



        public async Task<ICollection<UserModel>> GetAll()
        {
            try
            {
                var users = await _userRepository.GetAll();
                var userModels = users.Select(user => new UserModel
                {
                    Id = user.Id,
                    Mat = user.Mat,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Password = user.Password,
                    IdR = user.IdR,
                    IdDep = user.IdDep,
                    IdFf = user.IdFf,
                    IdIp = user.IdIp,
                    MatResp = user.MatResp,
                    Token = user.Token,
                    RefreshToken = user.RefreshToken,
                    RefreshTokenExpiryTime = user.RefreshTokenExpiryTime,
                }).ToList();

                return userModels;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all users", ex);
            }
        }


        public async Task<UserModel> GetUserByMat(int mat)
        {
            try
            {
                var user = await _userRepository.GetUserByMat(mat);

                var userModel = new UserModel
                {
                    Id = user.Id,
                    Mat = user.Mat,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Password = user.Password,
                    IdR = user.IdR,
                    IdDep = user.IdDep,
                    IdFf = user.IdFf,
                    IdIp = user.IdIp,
                    MatResp = user.MatResp,
                    Token =user.Token,
                    RefreshToken =user.RefreshToken,
                    RefreshTokenExpiryTime = user.RefreshTokenExpiryTime,
                };

                return userModel;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the user with Mat {mat}", ex);
            }
        }


        public async Task<List<UserModel>> RechFiltreAsync(Expression<Func<UserModel, bool>> predicate)
        {
            try
            {
                var users = await _userRepository.RechFiltreAsync(user => true);

                var userModels = users.Select(user => new UserModel
                {
                    Mat = user.Mat,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Password = user.Password,
                    IdR = user.IdR,
                    IdDep = user.IdDep,
                    IdFf = user.IdFf,
                    IdIp = user.IdIp,
                    MatResp = user.MatResp,
                    Token = user.Token,
                    RefreshToken = user.RefreshToken,
                    RefreshTokenExpiryTime = user.RefreshTokenExpiryTime,
                }).ToList();

                return userModels.AsQueryable().Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while filtering users", ex);
            }
        }


        public async Task<bool> UpdateUserAsync(UserModel user)
        {
            try
            {
                User userToUpdate = new User
                {
                    Id = user.Id,
                    Mat = user.Mat,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Password = user.Password,
                    IdR = user.IdR,
                    IdDep = user.IdDep,
                    IdFf = user.IdFf,
                    IdIp = user.IdIp,
                    MatResp = user.MatResp,
                    Token = user.Token,
                    RefreshToken = user.RefreshToken,
                    RefreshTokenExpiryTime = user.RefreshTokenExpiryTime,
                };

                return await _userRepository.UpdateUserAsync(userToUpdate);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the user", ex);
            }
        }



        public async Task<bool> AttributeFiche(int Mat, int IdFf)
        {
            var user = await _userRepository.GetUserByMat(Mat);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            var fiche = await _FfRepo.GetById(IdFf);
            if (fiche == null)
            {
                throw new KeyNotFoundException("fiche not found");
            }

            // Nassigni role lel user
            user.IdFf = IdFf;
            await _userRepository.UpdateUserAsync(user);

            return true;
        }

        public async Task<List<UserModel>> GetUserByName(string firstName)
        {
            try
            {
                var users = await _userRepository.GetUserByName(firstName);
                return users.Select(user => new UserModel
                {
                    Id = user.Id,
                    Mat = user.Mat,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Password = user.Password,
                    IdR = user.IdR,
                    IdDep = user.IdDep,
                    IdFf = user.IdFf,
                    IdIp = user.IdIp,
                    MatResp = user.MatResp,
                    Token = user.Token,
                    RefreshToken = user.RefreshToken,
                    RefreshTokenExpiryTime = user.RefreshTokenExpiryTime,
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving users by name {firstName}", ex);
            }
        }

        public async Task<List<UserModel>> GetUserByLastName(string lastName)
        {
            try
            {
                var users = await _userRepository.GetUserByLastName(lastName);
                return users.Select(user => new UserModel
                {
                    Id = user.Id,
                    Mat = user.Mat,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Password = user.Password,
                    IdR = user.IdR,
                    IdDep = user.IdDep,
                    IdFf = user.IdFf,
                    IdIp = user.IdIp,
                    MatResp = user.MatResp,
                    Token = user.Token,
                    RefreshToken = user.RefreshToken,
                    RefreshTokenExpiryTime = user.RefreshTokenExpiryTime,
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving users by name {lastName}", ex);
            }
        }



        public async Task<bool> AttributeIntegrationPlan(int Mat, int IdIp)
        {
            var user = await _userRepository.GetUserByMat(Mat);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            var plan = await _integrationPlanRepo.GetById(IdIp);
            if (plan == null)
            {
                throw new KeyNotFoundException("Integration Plan not found");
            }

            // Nassigni role lel user
            user.IdIp = IdIp;
            await _userRepository.UpdateUserAsync(user);

            return true;
        }


        public async Task<UserModel> GetUserByResp(int matResp)
        {
            try
            {
                var user = await _userRepository.GetUserByResp(matResp);

                var userModel = new UserModel
                {
                    Id = user.Id,
                    Mat = user.Mat,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Password = user.Password,
                    IdR = user.IdR,
                    IdDep = user.IdDep,
                    IdFf = user.IdFf,
                    IdIp = user.IdIp,
                    MatResp = user.MatResp,
                    Token = user.Token,
                    RefreshToken = user.RefreshToken,
                    RefreshTokenExpiryTime = user.RefreshTokenExpiryTime,
                };

                return userModel;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the user with Mat {matResp}", ex);
            }
        }

        public async Task<bool> AttributeResp(int Mat, int MatResp)
        {
            var user = await _userRepository.GetUserByMat(Mat);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            var fiche = await _userRepository.GetUserByMat(MatResp);
            if (fiche == null)
            {
                throw new KeyNotFoundException("Responsable not found");
            }

            // Nassigni role lel user
            user.MatResp = MatResp;
            await _userRepository.UpdateUserAsync(user);

            return true;
        }



        public async Task<string> LoginUser(int mat, string password)
        {
            try
            {
                var user = await _userRepository.GetUserByMat(mat);

                if (user == null)
                {
                    return "User with id " + mat + " not found";
                }

                var passwordHasher = new PasswordHasher<User>();
                var verificationResult = passwordHasher.VerifyHashedPassword(user, user.Password, password);

                if (verificationResult == PasswordVerificationResult.Failed)
                {
                    return "Password incorrect";
                }
                else
                {
                    // You can return a success message, a token, or other relevant data here
                    return "Success";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred during login", ex);
            }
        }


        

        public async Task<UserFfModel> GetFicheByMat(int mat)
        {
            try
            {
                var user = await _userRepository.GetUserByMat(mat);

                var userModel = new UserFfModel
                {
                    IdFf = user.IdFf,

                };

                return userModel;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the user with Mat {mat}", ex);
            }
        }
    }

}

