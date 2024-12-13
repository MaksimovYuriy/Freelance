using FreelanceDB.Abstractions.Services;
using FreelanceDB.Contracts.Requests;
using FreelanceDB.Contracts.Responses;
using FreelanceDB.Database.Entities;
using FreelanceDB.RabbitMQ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceDB.Controllers
{

    /// <summary>
    /// Контроллер для работы с данными из таблицы User
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService service;
        private readonly IRabbitMqService rabbitMqService;

        public UserController(IUserService service, IRabbitMqService rabbitMq)
        {
            this.service = service;
            rabbitMqService = rabbitMq;
        }

        /// <summary>
        /// Используется при регистрации для проверки уникальности логина
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<bool>> ChekUser(string login)
        {
            return Ok(await service.ChekUser(login));
        }

        /// <summary>
        /// Создание пользователя
        /// </summary>
        /// <response code="200">Пользователь создан</response>
        /// <response code="404">Логин занят</response>
        [HttpPost]
        public async Task<ActionResult<long>> CreateUser([FromBody] SignUpRequest request)
        {
            var id = await service.CreateUser(request);
            if (id == 0)
            {
                return BadRequest("Логин занят");
            }
            else
            {
                rabbitMqService.SendMessage(id);
                return Ok(id);
            }
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteUser(long id)
        {
            return Ok(await service.DeleteUser(id));
        }

        /// <summary>
        /// Вход пользователя в систему с помощью логина и пароля
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<UserResponse>> GetUserByLogin([FromBody] LoginRequest request)
        {
            var user = await service.GetUser(request.login, request.password);
            var response = new UserResponse(user.Id, user.Nickname, user.AToken, user.RToken);

            return Ok(response);
        }

        /// <summary>
        /// Получение данных пользователя по id
        /// </summary>
        [Authorize]
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
