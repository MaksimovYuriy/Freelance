using FreelanceDB.Contracts.Requests.UserRequests;
using FreelanceDB.Contracts.Responses.UserResponses;
using FreelanceDB.Database.Entities;
using FreelanceDB.RabbitMQ;
using FreelanceDB.Services.Services;
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
        private readonly ILogger<UserController> _logger;
        private readonly IRabbitMqService rabbitMqService;

        public UserController(IUserService service, IRabbitMqService rabbitMq, ILogger<UserController> logger)
        {
            this.service = service;
            rabbitMqService = rabbitMq;
            _logger = logger;
        }

        /// <summary>
        /// Используется при регистрации для проверки уникальности логина
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<bool>> ChekUser([FromQuery] CheckUserRequest request)
        {
            var result = await service.ChekUser(request.login);
            UserCheckResponse response = new UserCheckResponse(isChecked: result);
            _logger.LogInformation($"Checked user: {request.login} " + DateTime.Now.ToString());
            return Ok(response);
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
                _logger.LogInformation($"Trying to create user with existing login: {request.Login} " + DateTime.Now.ToString());
                return BadRequest("Логин занят");
            }
            else
            {
                rabbitMqService.SendMessage(id);
                CreateUserResponse response = new CreateUserResponse(newUserId: id);
                _logger.LogInformation($"Create user: {id} " + DateTime.Now.ToString());
                return Ok(response);
            }
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteUser(DeleteUserRequest request)
        {
            var status = await service.DeleteUser(request.userId);
            DeleteUserResponse response = new DeleteUserResponse(isDeleted: status);

            if(status == true)
            {
                _logger.LogInformation($"Delete user: {request.userId} " + DateTime.Now.ToString());
                return Ok(response);
            }

            _logger.LogWarning($"User: {request.userId} is not existing " + DateTime.Now.ToString());
            return NotFound(response);
        }

        /// <summary>
        /// Вход пользователя в систему с помощью логина и пароля
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<UserResponse>> GetUserByLogin([FromBody] LoginRequest request)
        {
            var user = await service.GetUser(request.login, request.password);
            var response = new UserResponse(user.Id, user.Nickname, user.AToken, user.RToken);

            if (user != null)
            {
                _logger.LogInformation($"Get user by login: {request.login} " + DateTime.Now.ToString());
                return Ok(response);
            }

            _logger.LogWarning($"Trying to get user by login: {request.login}" + DateTime.Now.ToString());
            return NotFound(response);
        }

        /// <summary>
        /// Получение данных пользователя по id
        /// </summary>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserResponse>> GetUserById([FromQuery] UserByIdRequest request)
        {
            var user = await service.GetUser(request.userId);
            //конверация в userresponse
            var response = new UserResponse(user.Id, user.Nickname, user.AToken, user.RToken);

            if (user != null)
            {
                _logger.LogInformation($"Get user by id: {request.userId} " + DateTime.Now.ToString());
                return Ok(response);
            }

            _logger.LogWarning($"Trying to get user by login: {request.userId}" + DateTime.Now.ToString());
            return NotFound(response);
        }
    }
}
