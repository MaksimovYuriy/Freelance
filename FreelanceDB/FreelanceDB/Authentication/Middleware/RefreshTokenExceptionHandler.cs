using FreelanceDB.Authentication.Abstractions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Security.Claims;

namespace FreelanceDB.Authentication.Middleware
{
    public class RefreshTokenExceptionHandler : ExceptionHandlerMiddleware
    {
        private readonly ITokenService tokenService;

        private readonly RequestDelegate Next;
        public RefreshTokenExceptionHandler(RequestDelegate next, ILoggerFactory loggerFactory, IOptions<ExceptionHandlerOptions> options, DiagnosticListener diagnosticListener, ITokenService tokenService) : base(next, loggerFactory, options, diagnosticListener)
        {
            this.tokenService = tokenService;
            Next = next;
        }

        public async new Task Invoke(HttpContext context)
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

        private async Task HandleRefreshTokenAsync(HttpContext context)
        {
            // Получите токен обновления из заголовков запроса.
            string accessToken = context.Request.Headers["Authorization"];

            ClaimsPrincipal claims = tokenService.GetPrincipalFromExpiredToken(accessToken);
            var idclaim = claims.FindFirst(ClaimTypes.NameIdentifier);
            long id = long.Parse(idclaim.Value);
            // Обновите токен доступа и токен обновления.
            var newToken = await tokenService.RefreshAccessToken(id);
            if (newToken =="") 
            {
                //отправить 401
                context.Response.StatusCode = 401;
               // context.Response.Body = "{\"error\": \"Refresh token has expired\"}";
            }
            else
            {
                // Установите новые токены в заголовки ответа.
                context.Response.Headers.Add("Authorization", $"Bearer {newToken}");
                context.Response.Headers.Add("RefreshToken", newToken);

                // Перенаправьте запрос на исходный ресурс.
                context.Response.StatusCode = 200;
                await Next(context);
            }
        }
    }
}
