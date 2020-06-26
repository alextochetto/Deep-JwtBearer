using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]")]
    public class PolicyController : ControllerBase
    {
        [HttpGet]
        [Route("policy")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trusted")]
        public ActionResult<dynamic> GetPolicy()
        {
            return true;
        }

        [HttpGet]
        [Route("rolemanager")]
        [Authorize(Roles = "Manager")]
        public ActionResult<dynamic> GetRoleManager()
        {
            return true;
        }

        [HttpGet]
        [Route("roles")]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult<dynamic> GetRole()
        {
            return true;
        }

        [HttpGet]
        [Route("create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "CanCreate")]
        public ActionResult<dynamic> Create()
        {
            return true;
        }
    }
}