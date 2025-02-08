using Asp.Versioning;
using EmployeePortal.Data.DataContext;
using EmployeePortal.Data.DataModel;
using EmployeePortal.Data.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace EmployeePortal.Controllers
{
    [Route("api/v{version:apiVersion}/[Controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Authorize]
    public class LoginController : ControllerBase
    {

        #region Constrctor


        public readonly ILoginRepository _ILoginRepository;
        public readonly IConfiguration _config;
        public readonly EmployeeContext _dbContext;
        private readonly IMemoryCache _cache;

        public LoginController(ILoginRepository iLoginRepository, IConfiguration config, EmployeeContext dbContext, IMemoryCache cache)
        {
            _ILoginRepository = iLoginRepository;
            _config = config;
            _dbContext = dbContext;
            _cache = cache;

        }
        #endregion Constrctor

        #region GetUserDetails

        /// <summary>
        /// GetUserDetails
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUserDetails")]
        public ActionResult<Response> Login()
        {
            Response result = new Response();
            {
                try
                {
                    result.StatusCode = 200;
                    result.Message = "success";
                    result.Result = _ILoginRepository.GetUsers().ToList();

                }
                catch (Exception ex)
                {
                    result.StatusCode = 500;
                    result.Message = ex.Message;

                }
                return result;
            }
        }

        #endregion

        #region GetUserRoles

        /// <summary>
        /// GetUserRoles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUserRoles")]
        [MapToApiVersion("1.0")]
        [ResponseCache(Duration = 60)]
        public ActionResult<Response> GetUserRoles()
        {
            Response result = new Response();
            {
                try
                {
                    result.StatusCode = 200;
                    result.Message = "success";
                    result.Result = _ILoginRepository.GetUserRoles().ToList();

                }
                catch (Exception ex)
                {
                    result.StatusCode = 500;
                    result.Message = ex.Message;

                }
                return result;
            }

        }

        #endregion

        #region ValidateUsers
        /// <summary>
        /// ValidateUsers
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ValidateUsers")]
        [AllowAnonymous]
        public ActionResult<Response> ValidateUsers([FromBody] User objUser)

        {
            try
            {

                bool isExists = _ILoginRepository.CheckUsersExists(objUser);

                if (isExists)
                {
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
                      _config["Jwt:Issuer"],
                      null,
                      expires: DateTime.Now.AddMinutes(120),
                      signingCredentials: credentials);

                    var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

                    return new Response
                    {
                        StatusCode = 200,
                        Message = "success",
                        Result = token
                    };
                }

                else
                {
                    return new Response
                    {
                        StatusCode = 401,
                        Message = "Invalid Username /Password",
                        Result = ""
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response
                {
                    StatusCode = 401,
                    Message = ex.Message,
                    Result = ""
                };

            }

        }

        #endregion


        #region Add User
        [HttpPatch]
        [Route("AddUsers")]
        [AllowAnonymous]
        public ActionResult<Response> AddUsers([FromBody] User objUser)

        {
            try
            {

                bool isExists = _ILoginRepository.SaveUsers (objUser);

                if (isExists)
                {
                   

                    return new Response
                    {
                        StatusCode = 200,
                        Message = "success",
                        Result = isExists
                    };
                }

                else
                {
                    return new Response
                    {
                        StatusCode = 401,
                        Message = "Error while saving",
                        Result = ""
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response
                {
                    StatusCode = 401,
                    Message = ex.Message,
                    Result = ""
                };

            }

        }

        #endregion End User
    }
}
