using EmployeePortal.Data.DataModel;
using EmployeePortal.Data.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace EmployeePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoginController : ControllerBase
    {
        public readonly ILoginRepository _ILoginRepository;
        public readonly IConfiguration _config;
        public LoginController(ILoginRepository iLoginRepository, IConfiguration config)
        {
            _ILoginRepository = iLoginRepository;
            _config = config;
        }


        [HttpGet]
        [Route("GetUserDetails")]

        public ActionResult<IEnumerable<User>> Login()
        {
            return _ILoginRepository.GetUsers();


        }

        [HttpPost]
        [Route("ValidateUsers")]
        [AllowAnonymous]
        public IActionResult ValidateUsers([FromBody] User objUser)
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

                return Ok(new Response
                {
                    StatusCode = 200,
                    Message = "success",
                    Result = token
                });
            }

            else
            {
                return Ok(new Response
                {
                    StatusCode = 401,
                    Message = "Invalid Username /Password",
                    Result = ""
                });
            }


        }

    }
}
