using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]")]
    public class AccessController : ControllerBase
    {
        [HttpGet]
        [Route("Any")]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult<dynamic> GetAny()
        {
            return true;
        }
        
        [HttpGet]
        [Route("Admin")]
        [Authorize(Roles = "Admin")]
        public ActionResult<dynamic> GetAdmin()
        {
            return new {User};
        }
        
        [HttpGet]
        [Route("Manager")]
        [Authorize(Roles = "Manager")]
        public ActionResult<dynamic> GetManager()
        {
            return new {User};
        }
        
        [HttpGet]
        [Route("Other")]
        [Authorize(Roles = "Analyst")]
        public ActionResult<dynamic> GetOther()
        {
            return new {User};
        }
    }
}