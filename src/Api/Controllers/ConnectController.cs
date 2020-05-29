using System.Threading.Tasks;
using Api.Models;
using Api.Repositories;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]")]
    [AllowAnonymous]
    public class ConnectController : ControllerBase
    {
        [HttpPost]
        [Route("Token")]
        public ActionResult<dynamic> GetToken([FromBody] UserModel model)
        {
            var user = UserRepository.Get(model.Username, model.Password);

            if (user is null)
                return NotFound(new { Message = "Invalid user or password" });

            var token = TokenService.Generate(user);
            user.Password = string.Empty;

            return new { User = user, Token = token };
        }
    }
}