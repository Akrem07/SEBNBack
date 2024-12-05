using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SEBNBack.Models;
using System.ComponentModel.DataAnnotations;
using SEBNBack.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SEBNBack.Models.dto;
using SebnLibrary.ModelEF;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity;
using SebnLibrary.Repo.Interfaces;



namespace SEBNBack.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly SEBNDbLibDbContext _authContext;

        private readonly IUserRepo _userRepository;

        public UserController(IUserService userService, SEBNDbLibDbContext context, IUserRepo userRepository)
        {
            this._userService = userService;
            _authContext = context;
            _userRepository = userRepository;
        }



        [AllowAnonymous]
        [Route("AddUsers")]
        [HttpPost]
        public async Task<ActionResult<String>> AddUserAsync([Required] AddUserModel user)
        {
            bool res = false;

            res = await _userService.AddUserAsync(user);
            if (res)
            {
                return Ok("User added");
            }
            else
            {
                return BadRequest("User not added");
            }
        }


        [AllowAnonymous]
        [Route("DeleteUser")]
        [HttpDelete]
        public async Task<ActionResult<string>> DeleteUserAsync([Required] int Mat)
        {
            bool res = await _userService.DeleteUserAsync(Mat);
            if (res)
            {
                return Ok("User deleted");
            }
            else
            {
                return NotFound("User not found");
            }
        }


        [AllowAnonymous]
        [Route("EditUser")]
        [HttpPut]
        public async Task<ActionResult<string>> EditUserAsync([Required] AddUserModel user)
        {
            bool res = await _userService.EditUserAsync(user);
            if (res)
            {
                return Ok("User updated");
            }
            else
            {
                return NotFound("User not found or not updated");
            }
        }


        [AllowAnonymous]

        [Route("GetAllUsers")]
        [HttpGet]
        public async Task<ActionResult<ICollection<UserModel>>> GetAllUsersAsync()
        {
            var users = await _userService.GetAll();
            if (users != null && users.Count > 0)
            {
                return Ok(users);
            }
            else
            {
                return NotFound("No users found");
            }
        }

        [AllowAnonymous]
        [Route("GetUserByMat/{mat}")]
        [HttpGet]
        public async Task<ActionResult<UserModel>> GetUserByMatAsync(int mat)
        {
            try
            {
                var userModel = await _userService.GetUserByMat(mat);
                return Ok(userModel);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [AllowAnonymous]
        [Route("GetUserByName/{FirstName}")]
        [HttpGet]
        public async Task<ActionResult<UserModel>> GetUserByNameAsync(string FirstName)
        {
            try
            {
                var userModel = await _userService.GetUserByName(FirstName);
                return Ok(userModel);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [AllowAnonymous]
        [Route("GetUserByLastName/{LastName}")]
        [HttpGet]
        public async Task<ActionResult<UserModel>> GetUserByLastNameAsync(string LastName)
        {
            try
            {
                var userModel = await _userService.GetUserByLastName(LastName);
                return Ok(userModel);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [AllowAnonymous]
        [Route("AttributeFiche")]
        [HttpPost]

        public async Task<IActionResult> AttributeFiche([FromBody] AttributeFicheRequest request)
        {
            try
            {
                await _userService.AttributeFiche(request.Mat, request.IdFf);
                return Ok("Fiche Fonction attributed successfully");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        public class AttributeFicheRequest
        {
            public int Mat { get; set; }
            public int IdFf { get; set; }
        }

        [AllowAnonymous]
        [Route("AttributeIntegrationPlan")]
        [HttpPost]
        public async Task<IActionResult> AttributeIntegrationPlan([FromBody] AttributeIntegrationPlanRequest request)
        {
            try
            {
                await _userService.AttributeIntegrationPlan(request.Mat, request.IdIp);
                return Ok("Integration Plan attributed successfully");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        public class AttributeIntegrationPlanRequest
        {
            public int Mat { get; set; }
            public int IdIp { get; set; }
        }



        [AllowAnonymous]
        [Route("AttributeResp")]
        [HttpPost]
        public async Task<IActionResult> AttributeResp([FromBody] AttributeRespRequest request)
        {
            try
            {
                await _userService.AttributeResp(request.Mat, request.MatResp);
                return Ok("Responsable attributed successfully");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        public class AttributeRespRequest
        {
            public int Mat { get; set; }
            public int MatResp { get; set; }
        }



        [AllowAnonymous]

        [Route("Login")]
        [HttpPost]

        public async Task<IActionResult> Login([Required] int mat, [Required] string Password)
        {
            var loginResult = await LoginUser(mat, Password);

            if (loginResult == "User with id " + mat + " not found")
            {
                return NotFound(new { Message = loginResult });
            }

            if (loginResult == "Password incorrect")
            {
                return BadRequest(new { Message = loginResult });
            }

            if (loginResult == "Success")
            {
                var user = await _authContext.Users.FirstOrDefaultAsync(x => x.Mat == mat);

                if (user == null)
                {
                    return NotFound(new { Message = $"User with id {mat} not found!" });
                }

                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, mat.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKeyForJwtTokenEncryption"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(30), // Token expiry time
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var newAccessToken = tokenHandler.WriteToken(token);

                var newRefreshToken = CreateRefreshToken();

                user.Token = newAccessToken;
                user.RefreshToken = newRefreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(5);

                await _authContext.SaveChangesAsync();

                return Ok(new TokenApiDto()
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken
                });
            }

            return Unauthorized();
        }





        private async Task<string> LoginUser(int mat, string password)
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

                return "Success";
            }
            catch (Exception ex)
            {
                // Log exception here
                throw;
            }
        }






        //[AllowAnonymous]

        //[Route("Authenticate")]
        //[HttpPost]
        //public async Task<IActionResult> Authenticate([FromBody] User1Model userObj)
        //{
        //    if (userObj == null)
        //        return BadRequest();

        //    var user = await _authContext.Users
        //        .FirstOrDefaultAsync(x => x.Mat == userObj.Mat);

        //    if (user == null)
        //        return NotFound(new { Message = "User not found!" });

        //    if (!PasswordHasher.VerifyPassword(userObj.Password, user.Password))
        //    {
        //        return BadRequest(new { Message = "Password is Incorrect" });
        //    }

        //    user.Token = CreateJwt(user);  // Line 287
        //                                   // Line 287
        //    var newAccessToken = user.Token;
        //    var newRefreshToken = CreateRefreshToken();
        //    user.RefreshToken = newRefreshToken;
        //    user.RefreshTokenExpiryTime = DateTime.Now.AddDays(5);
        //    await _authContext.SaveChangesAsync();

        //    return Ok(new TokenApiDto()
        //    {
        //        AccessToken = newAccessToken,
        //        RefreshToken = newRefreshToken
        //    });
        //}





        private static string CheckPasswordStrength(string pass)
        {
            StringBuilder sb = new StringBuilder();
            if (pass.Length < 9)
                sb.Append("Minimum password length should be 8" + Environment.NewLine);
            if (!(Regex.IsMatch(pass, "[a-z]") && Regex.IsMatch(pass, "[A-Z]") && Regex.IsMatch(pass, "[0-9]")))
                sb.Append("Password should be AlphaNumeric" + Environment.NewLine);
            if (!Regex.IsMatch(pass, "[<,>,@,!,#,$,%,^,&,*,(,),_,+,\\[,\\],{,},?,:,;,|,',\\,.,/,~,`,-,=]"))
                sb.Append("Password should contain special charcter" + Environment.NewLine);
            return sb.ToString();
        }


        private string CreateJwt(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryverysceret.....");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role,$"{user.IdR}"),
                new Claim(ClaimTypes.Name,$"{user.Mat}")
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddSeconds(10),
                SigningCredentials = credentials
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
        private string CreateRefreshToken()
        {
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var refreshToken = Convert.ToBase64String(tokenBytes);

            var tokenInUser = _authContext.Users
                .Any(a => a.RefreshToken == refreshToken);
            if (tokenInUser)
            {
                return CreateRefreshToken();
            }
            return refreshToken;
        }

        private ClaimsPrincipal GetPrincipleFromExpiredToken(string token)
        {
            var key = Encoding.ASCII.GetBytes("veryverysceret.....");
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("This is Invalid Token");
            return principal;

        }

        

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenApiDto tokenApiDto)
        {
            if (tokenApiDto is null)
                return BadRequest("Invalid Client Request");
            string accessToken = tokenApiDto.AccessToken;
            string refreshToken = tokenApiDto.RefreshToken;
            var principal = GetPrincipleFromExpiredToken(accessToken);
            var matricule = principal.Identity.Name;
            var user = await _authContext.Users.FirstOrDefaultAsync(u => u.Mat == int.Parse(matricule));
            // Line 390

            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest("Invalid Request");
            var newAccessToken = CreateJwt(user);  // Line 393
            var newRefreshToken = CreateRefreshToken();
            user.RefreshToken = newRefreshToken;
            await _authContext.SaveChangesAsync();
            return Ok(new TokenApiDto()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
            });
        }





        [AllowAnonymous]
        [Route("GetPlanByMat/{mat}")]
        [HttpGet]
        public async Task<ActionResult<UserFfModel>> GetPlanByMatAsync(int mat)
        {
            try
            {
                var userModel = await _userService.GetFicheByMat(mat);
                return Ok(userModel);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


    }
}
