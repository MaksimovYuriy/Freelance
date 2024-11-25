using FreelanceDB.Abstractions.Services;
using FreelanceDB.Authentication.Abstractions;
using FreelanceDB.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FreelanceDB.Authentication
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        public TokenController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }
        [HttpGet]
        //TODO: refresh()- обновлять токен
        public async Task<ActionResult<string>> Refresh(int id, string refreshToken) 
        {
            var data = await _userService.GetRTokenAndExpiryTimeAndRole(id);
            var expiryTime = data.Item2;
            var validRToken = data.Item1;
            var role = data.Item3;

            if (expiryTime <= DateTime.UtcNow)
            {
                await _userService.RemoveTokens(id);//разлогинить если истек рефреш
                return Unauthorized("Refresh token has expired");
            }
            else if (validRToken != refreshToken)
            {
                await _userService.RemoveTokens(id);//разлогинить если прислали не тот рефреш
                return Unauthorized("Refresh token is invalid");
            }
            else
            {
                var newToken = _tokenService.GenerateAccessToken(id, role);
                await _userService.UpdateUsersAToken(id, newToken);  //сохранить новые токены
                return Ok(newToken);
            }
        }
        
    }
}
