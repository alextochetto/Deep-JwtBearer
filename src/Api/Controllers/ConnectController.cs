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
        private readonly ITokenService _tokenService;
        public ConnectController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        
        [HttpPost]
        [Route("Token")]
        public async Task<ActionResult<dynamic>> GetToken([FromBody] UserModel model)
        {
            var user = UserRepository.Get(model.Username, model.Password);

            if (user is null)
                return NotFound(new { Message = "Invalid user or password" });

            var token = await _tokenService.Generate(user);
            user.Password = string.Empty;

            return new { User = user, Token = token };
        }
    }
}