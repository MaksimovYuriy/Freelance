using FreelanceDB.Abstractions.Services;
using FreelanceDB.Contracts.Requests;
using FreelanceDB.Contracts.Responses;
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
        //используется при регистрации для проверки уникальности логина
        public async Task<ActionResult<bool>> ChekUser(string login)
        {
            return Ok(await service.ChekUser(login));
        }

        [HttpPost]
        public async Task<ActionResult<long>> CreateUser([FromBody] SignUpRequest request)
        {

            return Ok(await service.CreateUser(request));
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteUser(long id)
        {
            return Ok(await service.DeleteUser(id));
        }

        [HttpPost]
        public async Task<ActionResult<UserResponse>> GetUserByLogin([FromBody] LoginRequest request)
        {
            var user = await service.GetUser(request.login, request.password);
            //конверация в userresponse
            var response = new UserResponse(user.Id, user.Nickname, user.AToken, user.RToken);//создать токены

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
