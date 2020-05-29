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
    }
}