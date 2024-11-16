using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [Route("validateUsers"),HttpGet]
        public ActionResult<string> ValidateUsers()
        {

            return "Success";
        }

    }
}
