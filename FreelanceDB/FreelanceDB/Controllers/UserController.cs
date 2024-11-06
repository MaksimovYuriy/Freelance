using FreelanceDB.Abstractions.Services;
using FreelanceDB.Contracts;
using FreelanceDB.Database.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceDB.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService service;

        public UserController(IUserService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<bool>> ChekUser(string login)
        {
            return await service.ChekUser(login);
        }

        [HttpPost]
        public async Task<ActionResult<long>> CreateUser([FromBody] UserRequest request)
        {

            return await service.CreateUser(request);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteUser(long id)
        {
            return await service.DeleteUser(id);
        }

        [HttpGet]
        public async Task<ActionResult<UserResponse>> GetUserByLogin(string login, string password)
        {
            var user = await service.GetUser(login, password);
            //конверация в userresponse
            var response = new UserResponse(user.Id, user.Nickname, user.AToken, user.RToken);

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<UserResponse>> GetUserById(long id)
        {
            var user = await service.GetUser(id);
            //конверация в userresponse
            var response = new UserResponse(user.Id, user.Nickname, user.AToken, user.RToken);

            return Ok(response);
        }
    }
}
