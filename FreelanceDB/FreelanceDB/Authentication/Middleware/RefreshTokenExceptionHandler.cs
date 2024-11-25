using FreelanceDB.Abstractions.Services;
using FreelanceDB.Authentication.Abstractions;
using FreelanceDB.Database.Entities;
using FreelanceDB.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Security.Claims;

namespace FreelanceDB.Authentication.Middleware
{
    public class RefreshTokenExceptionHandler //: ExceptionHandlerMiddleware, IExceptionHandler
    {
        private readonly ITokenService tokenService;
        //private readonly IUserService userService;
        private readonly IServiceProvider serviceProvider;

        private readonly RequestDelegate Next;
        public RefreshTokenExceptionHandler(RequestDelegate next, ITokenService tokenService, IServiceProvider serviceProvider) //: base(next, loggerFactory, options, diagnosticListener)
        {
            this.tokenService = tokenService;
            //this.userService = userService;
            this.serviceProvider = serviceProvider;
            Next = next;
        }

       

        public async System.Threading.Tasks.Task Invoke(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch (Exception ex)
            {
                if (ex is SecurityTokenExpiredException)
                {
                    // Токен доступа истек. Попробуйте его обновить.
                    await HandleRefreshTokenAsync(context);
                }
                else
                {
                    // Передать исключение вниз по конвейеру обработки.
                    throw;
                }
            }
        }

        private async System.Threading.Tasks.Task HandleRefreshTokenAsync(HttpContext context)
        {
            using (var scope = serviceProvider.CreateScope())//создание скоупа для передачи transient UserService в этот singlton middleware
            {
                var userService = scope.ServiceProvider.GetRequiredService<UserService>();

                // Получите токен обновления из заголовков запроса.
                string accessToken = context.Request.Headers["Authorization"];
                string refreshToken = context.Request.Headers["Refresh-Token"];

                ClaimsPrincipal claims = tokenService.GetPrincipalFromExpiredToken(accessToken);
                var idclaim = claims.FindFirst(ClaimTypes.NameIdentifier);
                long id = long.Parse(idclaim.Value);
                var roleClaim = claims.FindFirst(ClaimTypes.Role);
                string role = (roleClaim.Value).ToString();

                var data = await userService.GetRTokenAndExpiryTime(id);
                var expiryTime = data.Item2;
                var validRToken = data.Item1;

                if (expiryTime <= DateTime.UtcNow)
                {
                    userService.RemoveTokens(id);//разлогинить если истек рефреш
                    context.Response.StatusCode = 401;
                    // context.Response.Body = "{\"error\": \"Refresh token has expired\"}";
                }
                else if (validRToken != refreshToken)
                {
                    userService.RemoveTokens(id);//разлогинить если прислали не тот рефреш
                    context.Response.StatusCode = 401;
                    // context.Response.Body = "{\"error\": \"Refresh token is invalid\"}";
                }
                else
                {
                    var newToken = tokenService.GenerateAccessToken(id, role);
                    userService.UpdateUsersAToken(id, newToken);
                    // Установите новые токены в заголовки ответа.
                    context.Response.Headers.Add("Authorization", $"Bearer {newToken}");
                    context.Response.Headers.Add("RefreshToken", newToken);

                    //сохранить новые токены
                    // Перенаправьте запрос на исходный ресурс.
                    context.Response.StatusCode = 200;
                    await Next(context);
                }
            }
        }
    }
}
